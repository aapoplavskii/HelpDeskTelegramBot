using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Commands
{
    public class RegNewAppCommand
    {
        private IApplicationRepository _repositoryApplications;
        private IApplicationActionRepository _repositoryApplicationActions;
        private Dictionary<long, UserStates> _clientStates;
        private IRepositoryAdditionalDatabases<Building> _repositoryBuildings;
        private IRepositoryEmployees _repositoryEmployees;
        private IRepositoryAdditionalDatabases<Department> _repositoryDepartments;

        public RegNewAppCommand(IApplicationRepository repositoryApplications, IApplicationActionRepository repositoryApplicationActions, 
            Dictionary<long, UserStates> clientStates, IRepositoryAdditionalDatabases<Building> repositoryBuildings, IRepositoryEmployees repositoryEmployees,
            IRepositoryAdditionalDatabases<Department> repositoryDepartments)
        {
            _repositoryApplications = repositoryApplications;
            _repositoryApplicationActions = repositoryApplicationActions;
            _clientStates = clientStates;
            _repositoryBuildings = repositoryBuildings;
            _repositoryEmployees = repositoryEmployees; 
            _repositoryDepartments = repositoryDepartments;

        }
        public async Task RegNewApp(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId, Update update, Employee ouremployee)
        {
            var newappID = _clientStates[chatId].Value;

            var statewrite = _repositoryApplications.FindItem(_clientStates[chatId].Value).statewrite;
            var messageText = "";

            if (update.Message != null)
            {
                messageText = update.Message.Text;
            }

            switch (statewrite)
            {
                case 0:

                    _repositoryApplications.ChangeState(newappID, 1);

                    var buttonsActions = _repositoryApplicationActions.GetInlineKeyboardButtons();

                    await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Выберите тип заявки",
                                cancellationToken: cancellationToken,
                                replyMarkup: new InlineKeyboardMarkup(buttonsActions));
                    break;
                //TODO: сделать вывод всех inlinekeyboard и keyboardbutton через базу. Значения получать из таблиц и формировать List
                case 1:
                    _repositoryApplications.ChangeState(newappID, 2);

                    var buttonsBuildings = _repositoryBuildings.GetInlineKeyboardButtons();

                    await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Выберите номер корпуса",
                                cancellationToken: cancellationToken,
                                replyMarkup: new InlineKeyboardMarkup(buttonsBuildings));
                    break;
                case 2:
                    _repositoryApplications.ChangeState(newappID, 3);

                    await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Введите номер кабинета",
                                cancellationToken: cancellationToken);
                    _repositoryApplications.ChangeState(newappID, 3);
                    break;
                case 3:
                    _repositoryApplications.ChangeState(newappID, 4);

                    _repositoryApplications.UpdateRoomApp(newappID, messageText);

                    await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Введите контактный телефон",
                                cancellationToken: cancellationToken);
                    _repositoryApplications.ChangeState(newappID, 4);
                    break;
                case 4:
                    _repositoryApplications.ChangeState(newappID, 5);

                    _repositoryApplications.UpdatePhoneApp(newappID, messageText);

                    await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Введите текст заявки",
                                cancellationToken: cancellationToken);
                    _repositoryApplications.ChangeState(newappID, 5);
                    break;
                case 5:
                    _repositoryApplications.ChangeState(newappID, 6);

                    _repositoryApplications.UpdateContentApp(newappID, messageText);
                    _repositoryApplications.ChangeState(newappID, 6);


                    _repositoryApplicationActions.AddNewAppAction(_clientStates[chatId].Value, ouremployee.ID, 1);

                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ваша заявка направлена в IT отдел.\nДля новой задачи воспользуйтесь меню!",
                        replyMarkup: new ReplyKeyboardMarkup(new List<KeyboardButton>
                        {
                            new KeyboardButton("Подать новую заявку"),
                            new KeyboardButton("Посмотреть неисполненные заявки"),

                        })
                        {
                            ResizeKeyboard = true,
                            OneTimeKeyboard = true,
                        },
                        cancellationToken: cancellationToken);


                    await Command.SendMessageForTechEmployee(_clientStates[chatId].Value, ouremployee.ID, botClient, cancellationToken, _repositoryApplications,
                        _repositoryBuildings, _repositoryEmployees, _repositoryDepartments);

                    _clientStates[chatId] = new UserStates { State = State.none, Value = 0 };

                    break;


            }

        }

    }
}
