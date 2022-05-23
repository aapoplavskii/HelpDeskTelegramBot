using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Commands
{
    public class SubmitNewAppCommand : ICommand
    {
        private Employee _ouremployee;
        private CancellationToken _cancellationToken;
        private ITelegramBotClient _botClient;
        private IApplicationRepository _repositoryApplications;
        private IRepositoryEmployees _repositoryEmployees;
        private Dictionary<long, UserStates> _clientStates;
        private IRepositoryAdditionalDatabases<TypeApplication> _repositoryTypeApplication;
        private IRepositoryAdditionalDatabases<Building> _repositoryBuildings;
        private IRepositoryAdditionalDatabases<Department> _repositoryDepartments;
        private long _chatId;
        private IApplicationActionRepository _repositoryApplicationActions;


        public SubmitNewAppCommand(Employee employee, CancellationToken cancellationToken, ITelegramBotClient botClient,
            IApplicationRepository repositoryApplications, IRepositoryEmployees repositoryEmployees,
            Dictionary<long, UserStates> clientStates, IRepositoryAdditionalDatabases<TypeApplication> repositoryTypeApplication,
            IRepositoryAdditionalDatabases<Department> repositoryDepartments, IRepositoryAdditionalDatabases<Building> repositoryBuildings, long chatId,
            IApplicationActionRepository repositoryApplicationActions)
        {
            _ouremployee = employee;
            _cancellationToken = cancellationToken;
            _botClient = botClient;
            _repositoryApplications = repositoryApplications;
            _repositoryEmployees = repositoryEmployees;
            _clientStates = clientStates;
            _repositoryDepartments = repositoryDepartments;
            _repositoryBuildings = repositoryBuildings;
            _chatId = chatId;
            _repositoryTypeApplication = repositoryTypeApplication;
            _repositoryApplicationActions = repositoryApplicationActions;

        }


        public async Task<Response> Execute(Update update)
        {
            
            if (_clientStates[_chatId].State != State.newapp)
            {
                var newApp = _repositoryApplications.AddNewApp(_chatId, _repositoryEmployees);

                _clientStates[_chatId] = new UserStates { State = State.newapp, Value = newApp.ID };

                
            }
            
            var newappID = _clientStates[_chatId].Value;

            var statewrite = _repositoryApplications.FindItem(_clientStates[_chatId].Value).statewrite;
            var messageText = "";

            if (update.Message != null)
            {
                messageText = update.Message.Text;
            }

            switch (statewrite)
            {
                case 0:

                    _repositoryApplications.ChangeState(newappID, 1);

                    var buttonsActions = _repositoryTypeApplication.GetInlineKeyboardButtons();

                    await _botClient.SendTextMessageAsync(
                                chatId: _chatId,
                                text: "Выберите тип заявки",
                                cancellationToken: _cancellationToken,
                                replyMarkup: new InlineKeyboardMarkup(buttonsActions));
                    break;
                //TODO: сделать вывод всех inlinekeyboard и keyboardbutton через базу. Значения получать из таблиц и формировать List
                case 1:
                    _repositoryApplications.ChangeState(newappID, 2);

                    var buttonsBuildings = _repositoryBuildings.GetInlineKeyboardButtons();

                    await _botClient.SendTextMessageAsync(
                                chatId: _chatId,
                                text: "Выберите номер корпуса",
                                cancellationToken: _cancellationToken,
                                replyMarkup: new InlineKeyboardMarkup(buttonsBuildings));
                    break;
                case 2:
                    _repositoryApplications.ChangeState(newappID, 3);

                    await _botClient.SendTextMessageAsync(
                                chatId: _chatId,
                                text: "Введите номер кабинета",
                                cancellationToken: _cancellationToken);
                    _repositoryApplications.ChangeState(newappID, 3);
                    break;
                case 3:
                    _repositoryApplications.ChangeState(newappID, 4);

                    _repositoryApplications.UpdateRoomApp(newappID, messageText);

                    await _botClient.SendTextMessageAsync(
                                chatId: _chatId,
                                text: "Введите контактный телефон",
                                cancellationToken: _cancellationToken);
                    _repositoryApplications.ChangeState(newappID, 4);
                    break;
                case 4:
                    _repositoryApplications.ChangeState(newappID, 5);

                    _repositoryApplications.UpdatePhoneApp(newappID, messageText);

                    await _botClient.SendTextMessageAsync(
                                chatId: _chatId,
                                text: "Введите текст заявки",
                                cancellationToken: _cancellationToken);
                    _repositoryApplications.ChangeState(newappID, 5);
                    break;
                case 5:
                    _repositoryApplications.ChangeState(newappID, 6);

                    _repositoryApplications.UpdateContentApp(newappID, messageText);
                    _repositoryApplications.ChangeState(newappID, 6);


                    _repositoryApplicationActions.AddNewAppAction(_clientStates[_chatId].Value, _ouremployee.ID, 1);

                    await _botClient.SendTextMessageAsync(
                        chatId: _chatId,
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
                        cancellationToken: _cancellationToken);


                    await Command.SendMessageForTechEmployee(_clientStates[_chatId].Value, _ouremployee.ID, _botClient, _cancellationToken, _repositoryApplications,
                        _repositoryBuildings, _repositoryEmployees, _repositoryDepartments);

                    _clientStates[_chatId] = new UserStates { State = State.none, Value = 0 };

                    break;


            }

            
            return new Response { Message = "Новая заявка зарегистрирована." };
        }
    }
}
