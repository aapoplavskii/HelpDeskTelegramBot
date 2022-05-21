using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Commands
{
    public class RegNewUserCommand
    {
        private IRepositoryEmployees _repositoryEmployees = null;
        private IRepositoryAdditionalDatabases<PositionEmployee> _repositoryPositions = null;
        private IRepositoryAdditionalDatabases<Department> _repositoryDepartment = null;
        private Dictionary<long, UserStates> _clientStates = null;

        public RegNewUserCommand(IRepositoryEmployees repositoryEmployees, IRepositoryAdditionalDatabases<PositionEmployee> repositoryPositions,
            IRepositoryAdditionalDatabases<Department> repositoryDepartment, Dictionary<long, UserStates> clientStates)
        {
            _repositoryEmployees = repositoryEmployees;
            _repositoryPositions = repositoryPositions;
            _repositoryDepartment = repositoryDepartment;
            _clientStates = clientStates;
        }

        public async Task RegNewUser(ITelegramBotClient botClient, CancellationToken cancellationToken,
                                        long chatId, Update update, Employee ouremployee)
        {
            var stateuser = ouremployee.State;

            switch (stateuser)
            {
                case 0:

                    _repositoryEmployees.ChangeState(chatId, 1);

                    await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Для регистрации введите ФИО!",
                                cancellationToken: cancellationToken);

                    break;
                case 1:
                    _repositoryEmployees.UpdateFIOEmployee(chatId, update.Message.Text.ToString());
                    _repositoryEmployees.ChangeState(chatId, 2);

                    //TODO: перепроверить получение кнопок из базы, а также алгоритм построения линий кнопок
                    var buttonsPositions = _repositoryPositions.GetInlineKeyboardButtons();

                    await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Выберите должность",
                                cancellationToken: cancellationToken,
                                replyMarkup: new InlineKeyboardMarkup(buttonsPositions));

                    break;

                case 2:
                    _repositoryEmployees.ChangeState(chatId, 3);

                    var buttonsDepartments = _repositoryDepartment.GetInlineKeyboardButtons();

                    await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Выберите подразделение",
                                cancellationToken: cancellationToken,
                                replyMarkup: new InlineKeyboardMarkup(buttonsDepartments));

                    break;

                case 3:
                    _repositoryEmployees.ChangeState(chatId, 4);
                    await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Являетесь ли Вы исполнителем заявок?",
                                cancellationToken: cancellationToken,
                                replyMarkup: new InlineKeyboardMarkup(new List<InlineKeyboardButton>()
                {
                        InlineKeyboardButton.WithCallbackData("Да", "/да"),
                        InlineKeyboardButton.WithCallbackData("Нет", "/нет"),

                }));

                    break;
                case 4:
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
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
                        cancellationToken: cancellationToken);

                    _clientStates[chatId] = new UserStates { State = State.none, Value = 0 };

                    break;

                default:
                    break;

            }
        }

    }
}
