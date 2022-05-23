using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Commands
{
    public class RegNewUserCommand:ICommand
    {
        private IRepositoryEmployees _repositoryEmployees;
        private IRepositoryAdditionalDatabases<PositionEmployee> _repositoryPositions;
        private IRepositoryAdditionalDatabases<Department> _repositoryDepartment;
        private Dictionary<long, UserStates> _clientStates;
        private ITelegramBotClient _botclient;
        private CancellationToken _cancellationToken;
        private Update _update;
        private Employee _ouremployee;
        private long _chatId;

        public RegNewUserCommand(IRepositoryEmployees repositoryEmployees, IRepositoryAdditionalDatabases<PositionEmployee> repositoryPositions,
            IRepositoryAdditionalDatabases<Department> repositoryDepartment, Dictionary<long, UserStates> clientStates, ITelegramBotClient botClient,
            CancellationToken cancellationToken, Update update, Employee employee, long chatId)
        {
            _repositoryEmployees = repositoryEmployees;
            _repositoryPositions = repositoryPositions;
            _repositoryDepartment = repositoryDepartment;
            _clientStates = clientStates;
            _botclient = botClient;
            _cancellationToken = cancellationToken;
            _update = update;
            _ouremployee = employee;
            _chatId = chatId;
        }

        public async Task<Response> Execute(Update update)
        {
            
            var stateuser = _ouremployee.State;

            switch (stateuser)
            {
                case 0:

                    _repositoryEmployees.ChangeState(_chatId, 1);

                    await _botclient.SendTextMessageAsync(
                                chatId: _chatId,
                                text: "Для регистрации введите ФИО!",
                                cancellationToken: _cancellationToken);

                    break;
                case 1:
                    _repositoryEmployees.UpdateFIOEmployee(_chatId, update.Message.Text.ToString());
                    _repositoryEmployees.ChangeState(_chatId, 2);

                    //TODO: перепроверить получение кнопок из базы, а также алгоритм построения линий кнопок
                    var buttonsPositions = _repositoryPositions.GetInlineKeyboardButtons();

                    await _botclient.SendTextMessageAsync(
                                chatId: _chatId,
                                text: "Выберите должность",
                                cancellationToken: _cancellationToken,
                                replyMarkup: new InlineKeyboardMarkup(buttonsPositions));

                    break;

                case 2:
                    _repositoryEmployees.ChangeState(_chatId, 3);

                    var buttonsDepartments = _repositoryDepartment.GetInlineKeyboardButtons();

                    await _botclient.SendTextMessageAsync(
                                chatId: _chatId,
                                text: "Выберите подразделение",
                                cancellationToken: _cancellationToken,
                                replyMarkup: new InlineKeyboardMarkup(buttonsDepartments));

                    break;

                case 3:
                    _repositoryEmployees.ChangeState(_chatId, 4);
                    await _botclient.SendTextMessageAsync(
                                chatId: _chatId,
                                text: "Являетесь ли Вы исполнителем заявок?",
                                cancellationToken: _cancellationToken,
                                replyMarkup: new InlineKeyboardMarkup(new List<InlineKeyboardButton>()
                {
                        InlineKeyboardButton.WithCallbackData("Да", "/да"),
                        InlineKeyboardButton.WithCallbackData("Нет", "/нет"),

                }));

                    break;
                case 4:
                    await _botclient.SendTextMessageAsync(
                        chatId: _chatId,
                        text: "Выберите задачу",
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

                    _clientStates[_chatId] = new UserStates { State = State.none, Value = 0 };

                    break;

                default:
                    break;

            }

            return new Response { Message = "Новый пользователь зарегистрирован!" };

        }
               

    }
}
