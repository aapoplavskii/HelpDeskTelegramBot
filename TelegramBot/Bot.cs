using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public class Bot
    {
        private TelegramBotClient bot;

       
        public void InitBot()
        {
            bot = new TelegramBotClient(ChatBotSettings.token);

            using var cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            bot.StartReceiving(
                    HandleUpdateAsync,
                    HandleErrorAsync,
                    receiverOptions,
                    cancellationToken: cts.Token);

            Console.ReadKey();

            cts.Cancel();
        }


        private Task HandleErrorAsync(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            var ErrorMessage = arg2 switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => arg2.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            PositionEmployee position;
            Department department;
            //Building building;

            switch (update.Type)
            {

                case UpdateType.CallbackQuery:

                    var ouremployee = Program.RepositoryEmployees.FindItemChatID(update.CallbackQuery.Message.Chat.Id);

                    switch (update.CallbackQuery.Data)
                    {
                            

                        case "/медицина":

                            position = Program.RepositoryPositions.FindItem(1);
                            
                            Program.RepositoryEmployees.UpdatePositionEmployee(update.CallbackQuery.Message.Chat.Id, 3, position);
                            await GetStateUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, Program.RepositoryEmployees, update);

                            break;
                        case "/общие":

                            position = Program.RepositoryPositions.FindItem(2);

                            Program.RepositoryEmployees.UpdatePositionEmployee(update.CallbackQuery.Message.Chat.Id, 3, position);
                            await GetStateUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, Program.RepositoryEmployees, update);

                            break;
                        case "/наука":

                            position = Program.RepositoryPositions.FindItem(3);

                            Program.RepositoryEmployees.UpdatePositionEmployee(update.CallbackQuery.Message.Chat.Id, 3, position);
                            await GetStateUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, Program.RepositoryEmployees, update);

                            break;
                        case "/инженер":

                            position = Program.RepositoryPositions.FindItem(4);

                            Program.RepositoryEmployees.UpdatePositionEmployee(update.CallbackQuery.Message.Chat.Id, 3, position);
                            await GetStateUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, Program.RepositoryEmployees, update); 

                            break;

                        case "/администрация":

                            department = Program.RepositoryDepartment.FindItem(1);

                            Program.RepositoryEmployees.UpdateDepartmentEmployee(update.CallbackQuery.Message.Chat.Id, 4, department);
                            await GetStateUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, Program.RepositoryEmployees, update);

                            break;
                        case "/поликлиника":

                            department = Program.RepositoryDepartment.FindItem(2);

                            Program.RepositoryEmployees.UpdateDepartmentEmployee(update.CallbackQuery.Message.Chat.Id, 4, department);
                            await GetStateUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, Program.RepositoryEmployees, update);

                            break;
                        case "/клиника":

                            department = Program.RepositoryDepartment.FindItem(3);

                            Program.RepositoryEmployees.UpdateDepartmentEmployee(update.CallbackQuery.Message.Chat.Id, 4, department);
                            await GetStateUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, Program.RepositoryEmployees, update);

                            break;
                        case "/наука_отдел":

                            department = Program.RepositoryDepartment.FindItem(4);

                            Program.RepositoryEmployees.UpdateDepartmentEmployee(update.CallbackQuery.Message.Chat.Id, 4, department);
                            await GetStateUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, Program.RepositoryEmployees, update);

                            break;
                        case "/диагностика":

                            department = Program.RepositoryDepartment.FindItem(5);

                            Program.RepositoryEmployees.UpdateDepartmentEmployee(update.CallbackQuery.Message.Chat.Id, 4, department);
                            await GetStateUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, Program.RepositoryEmployees, update);

                            break;
                        case "/кафедра":

                            department = Program.RepositoryDepartment.FindItem(6);

                            Program.RepositoryEmployees.UpdateDepartmentEmployee(update.CallbackQuery.Message.Chat.Id, 4, department);
                            await GetStateUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, Program.RepositoryEmployees, update);
                            break;
                        case "/да":

                            Program.RepositoryEmployees.UpdateIsExecutorEmployee(update.CallbackQuery.Message.Chat.Id, 5, true);
                            await GetStateUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, Program.RepositoryEmployees, update);

                            break;
                        case "/нет":
                                                        
                            Program.RepositoryEmployees.UpdateIsExecutorEmployee(update.CallbackQuery.Message.Chat.Id, 5, false);
                            await GetStateUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, Program.RepositoryEmployees, update);

                            break;



                    }

                    Console.WriteLine($"Данные из запроса: {update.CallbackQuery.Data}");
                    break;

                case UpdateType.Message:
                    await HandleMessage(update, cancellationToken, botClient);
                    break;


            }


        }

        private async Task HandleMessage(Update update, CancellationToken cancellationToken, ITelegramBotClient botClient)
        {

            if (update.Type != UpdateType.Message)
                return;

            if (update.Message!.Type != MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;

            var messageText = update.Message.Text;



            var ouremployee = Program.RepositoryEmployees.FindItemChatID(chatId);

            if (ouremployee == null)
            {

                Program.RepositoryEmployees.AddNewEmployee(chatId);
                ouremployee = Program.RepositoryEmployees.FindItemChatID(chatId);
                await GetStateUser(ouremployee, botClient, cancellationToken, chatId, Program.RepositoryEmployees, update);

            }

            switch (messageText)

            {
                case "/start":
                                        
                        await GetStateUser(ouremployee, botClient, cancellationToken, chatId, Program.RepositoryEmployees, update);
                    
                    break;
                case "Подать новую заявку":
                    {
                        if (ouremployee == null || ouremployee.State != 5)
                        {
                            await GetStateUser(ouremployee, botClient, cancellationToken, chatId, Program.RepositoryEmployees, update);
                            break;
                        }
                        Command.SubmitNewApplication();

                        break;
                    }
                case "Посмотреть состояние своих заявок":
                {
                        if (ouremployee == null || ouremployee.State != 5)
                        {
                            await GetStateUser(ouremployee, botClient, cancellationToken, chatId, Program.RepositoryEmployees, update);
                            break;
                        }
                        Command.ViewMyApplication();

                    break;
                }
                default:
                    await GetStateUser(ouremployee, botClient, cancellationToken, chatId, Program.RepositoryEmployees, update);
                    break;

            }



        }

        private async Task GetStateUser(Employee ouremployee, ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId, RepositoryEmployees repositoryEmployees, Update update)
        {
            var stateuser = ouremployee.State;

            switch (stateuser)
            {
                case 0:
                    await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Вы не идентифицированы! Для регистрации введите ФИО!",
                                cancellationToken: cancellationToken);
                    repositoryEmployees.ChangeState(ouremployee, 1);
                    break;

                case 1:
                    repositoryEmployees.UpdateFIOEmployee(chatId, 2, update.Message.ToString());
                    
                    break;
                case 2:
                    await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Выберите должность",
                                cancellationToken: cancellationToken,
                                replyMarkup: new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>()
                {
                    new List<InlineKeyboardButton>() {

                        InlineKeyboardButton.WithCallbackData("Медицинский сотрудник", "/медицина"),
                        InlineKeyboardButton.WithCallbackData("Общебольничный персонал", "/общие")
                    },
                    new List<InlineKeyboardButton>() {
                        InlineKeyboardButton.WithCallbackData("Научный сотрудник", "/наука"),
                        InlineKeyboardButton.WithCallbackData("Медицинский инженер", "/инженер")
                    }
                }));
                    
                    break;
                case 3:
                    await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Выберите подразделение",
                                cancellationToken: cancellationToken,
                                replyMarkup: new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>()
                {
                    new List<InlineKeyboardButton>() {

                        InlineKeyboardButton.WithCallbackData("Администрация", "/администрация"),
                        InlineKeyboardButton.WithCallbackData("Поликлиника", "/поликлиника"),
                        InlineKeyboardButton.WithCallbackData("Клиника", "/клиника")
                    },
                    new List<InlineKeyboardButton>() {
                        InlineKeyboardButton.WithCallbackData("Наука", "/наука_отдел"),
                        InlineKeyboardButton.WithCallbackData("Диагностика", "/диагностика"),
                        InlineKeyboardButton.WithCallbackData("Кафедра", "/кафедра")
                    }
                }));
                    
                    break;
                case 4:
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
                case 5:
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Выберите задачу",
                        replyMarkup: new ReplyKeyboardMarkup(new List<KeyboardButton>
                        {
                            new KeyboardButton("Подать новую заявку"),
                            new KeyboardButton("Посмотреть состояние своих заявок"),

                        })
                        {
                            ResizeKeyboard = true,
                            OneTimeKeyboard = true,
                        },
                        cancellationToken: cancellationToken);

                    break;

                default:
                    break;

            }
        }
    }
}

