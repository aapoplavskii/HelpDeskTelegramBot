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
        private IApplicationRepository _repositoryApplications = null;
        private IApplicationActionRepository _repositoryApplicationActions = null;
        private Dictionary<long, UserStates> _clientStates = null;

        public RegNewAppCommand(IApplicationRepository repositoryApplications, IApplicationActionRepository repositoryApplicationActions, Dictionary<long, UserStates> clientStates)
        {
            _repositoryApplications = repositoryApplications;
            _repositoryApplicationActions = repositoryApplicationActions;
            _clientStates = clientStates;

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

                    await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Выберите тип заявки",
                                cancellationToken: cancellationToken,
                                replyMarkup: new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>()
                {
                    new List<InlineKeyboardButton>() {

                        InlineKeyboardButton.WithCallbackData("Ремонт техники", "/ремонт"),
                        InlineKeyboardButton.WithCallbackData("Проблемы с сетью", "/сеть")
                    },
                    new List<InlineKeyboardButton>() {
                        InlineKeyboardButton.WithCallbackData("Проблемы с МИС", "/МИС"),
                        InlineKeyboardButton.WithCallbackData("Просто спросить/прочее", "/прочее")
                    }
                }));
                    break;
                //TODO: сделать вывод всех inlinekeyboard и keyboardbutton через базу. Значения получать из таблиц и формировать List
                case 1:
                    _repositoryApplications.ChangeState(newappID, 2);

                    await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Выберите номер корпуса",
                                cancellationToken: cancellationToken,
                                replyMarkup: new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>()
                {
                    new List<InlineKeyboardButton>() {

                        InlineKeyboardButton.WithCallbackData("2.1", "/2.1"),
                        InlineKeyboardButton.WithCallbackData("2.2", "/2.2"),
                        InlineKeyboardButton.WithCallbackData("2.3", "/2.3")
                    },
                    new List<InlineKeyboardButton>() {
                        InlineKeyboardButton.WithCallbackData("3", "/3"),
                        InlineKeyboardButton.WithCallbackData("5", "/5"),
                        InlineKeyboardButton.WithCallbackData("7", "/7")
                    }
                }));
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


                    await Command.SendMessageForTechEmployee(_clientStates[chatId].Value, ouremployee.ID, botClient, cancellationToken);

                    _clientStates[chatId] = new UserStates { State = State.none, Value = 0 };

                    break;


            }

        }

    }
}
