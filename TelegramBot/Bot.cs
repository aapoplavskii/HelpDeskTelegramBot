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

        private Dictionary<long, UserStates> _clientStates = new Dictionary<long, UserStates>();

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
            Employee ouremployee;
            Building building;
            TypeApplication typeApplication;
            Application newapp;

            switch (update.Type)
            {

                case UpdateType.CallbackQuery:

                    ouremployee = Program.RepositoryEmployees.FindItemChatID(update.CallbackQuery.Message.Chat.Id);

                    if (ouremployee == null)
                    {

                        Program.RepositoryEmployees.AddNewEmployee(update.CallbackQuery.Message.Chat.Id);
                        ouremployee = Program.RepositoryEmployees.FindItemChatID(update.CallbackQuery.Message.Chat.Id);
                        _clientStates[update.CallbackQuery.Message.Chat.Id] = new UserStates { State = State.newemployee, Value = ouremployee.Id };

                    }

                    switch (update.CallbackQuery.Data)
                    {


                        case "/медицина":

                            position = Program.RepositoryPositions.FindItem(1);

                            Program.RepositoryEmployees.UpdatePositionEmployee(update.CallbackQuery.Message.Chat.Id, 2, position);
                            await RegNewUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update);

                            break;
                        case "/общие":

                            position = Program.RepositoryPositions.FindItem(2);

                            Program.RepositoryEmployees.UpdatePositionEmployee(update.CallbackQuery.Message.Chat.Id, 2, position);
                            await RegNewUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update);

                            break;
                        case "/наука":

                            position = Program.RepositoryPositions.FindItem(3);

                            Program.RepositoryEmployees.UpdatePositionEmployee(update.CallbackQuery.Message.Chat.Id, 2, position);
                            await RegNewUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update);

                            break;
                        case "/инженер":

                            position = Program.RepositoryPositions.FindItem(4);

                            Program.RepositoryEmployees.UpdatePositionEmployee(update.CallbackQuery.Message.Chat.Id, 2, position);
                            await RegNewUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update);

                            break;

                        case "/администрация":

                            department = Program.RepositoryDepartment.FindItem(1);

                            Program.RepositoryEmployees.UpdateDepartmentEmployee(update.CallbackQuery.Message.Chat.Id, 3, department);
                            await RegNewUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update);

                            break;
                        case "/поликлиника":

                            department = Program.RepositoryDepartment.FindItem(2);

                            Program.RepositoryEmployees.UpdateDepartmentEmployee(update.CallbackQuery.Message.Chat.Id, 3, department);
                            await RegNewUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update);

                            break;
                        case "/клиника":

                            department = Program.RepositoryDepartment.FindItem(3);

                            Program.RepositoryEmployees.UpdateDepartmentEmployee(update.CallbackQuery.Message.Chat.Id, 3, department);
                            await RegNewUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update);

                            break;
                        case "/наука_отдел":

                            department = Program.RepositoryDepartment.FindItem(4);

                            Program.RepositoryEmployees.UpdateDepartmentEmployee(update.CallbackQuery.Message.Chat.Id, 3, department);
                            await RegNewUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update);

                            break;
                        case "/диагностика":

                            department = Program.RepositoryDepartment.FindItem(5);

                            Program.RepositoryEmployees.UpdateDepartmentEmployee(update.CallbackQuery.Message.Chat.Id, 3, department);
                            await RegNewUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update);

                            break;
                        case "/кафедра":

                            department = Program.RepositoryDepartment.FindItem(6);

                            Program.RepositoryEmployees.UpdateDepartmentEmployee(update.CallbackQuery.Message.Chat.Id, 3, department);
                            await RegNewUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update);
                            break;
                        case "/да":

                            Program.RepositoryEmployees.UpdateIsExecutorEmployee(update.CallbackQuery.Message.Chat.Id, 4, true);
                            await RegNewUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update);

                            break;
                        case "/нет":

                            Program.RepositoryEmployees.UpdateIsExecutorEmployee(update.CallbackQuery.Message.Chat.Id, 4, false);
                            await RegNewUser(ouremployee, botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update);

                            break;
                        case "/ремонт":

                            typeApplication = Program.RepositoryTypeApplication.FindItem(1);

                            newapp = Program.RepositoryApplications.FindItem(_clientStates[update.CallbackQuery.Message.Chat.Id].Value);
                            
                            if (newapp != null)
                            Program.RepositoryApplications.UpdateTypeApp(newapp, typeApplication, 1);
                            
                            await RegNewApp(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/сеть":

                            typeApplication = Program.RepositoryTypeApplication.FindItem(2);

                            newapp = Program.RepositoryApplications.FindItem(_clientStates[update.CallbackQuery.Message.Chat.Id].Value);

                            if (newapp != null)
                                Program.RepositoryApplications.UpdateTypeApp(newapp, typeApplication, 1);
                            
                            await RegNewApp(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/МИС":

                            typeApplication = Program.RepositoryTypeApplication.FindItem(3);

                            newapp = Program.RepositoryApplications.FindItem(_clientStates[update.CallbackQuery.Message.Chat.Id].Value);

                            if (newapp != null)
                                Program.RepositoryApplications.UpdateTypeApp(newapp, typeApplication, 1);
                            
                            await RegNewApp(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/прочее":

                            typeApplication = Program.RepositoryTypeApplication.FindItem(4);

                            newapp = Program.RepositoryApplications.FindItem(_clientStates[update.CallbackQuery.Message.Chat.Id].Value);

                            if (newapp != null)
                                Program.RepositoryApplications.UpdateTypeApp(newapp, typeApplication, 1);

                            await RegNewApp(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;

                        case "/2.1":

                            building = Program.RepositoryBuildings.FindItem(1);

                            newapp = Program.RepositoryApplications.FindItem(_clientStates[update.CallbackQuery.Message.Chat.Id].Value);

                            if (newapp != null)
                                Program.RepositoryApplications.UpdateBuildingApp(newapp, building, 2);
                            
                            await RegNewApp(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/2.2":

                            building = Program.RepositoryBuildings.FindItem(2);

                            newapp = Program.RepositoryApplications.FindItem(_clientStates[update.CallbackQuery.Message.Chat.Id].Value);

                            if (newapp != null)
                                Program.RepositoryApplications.UpdateBuildingApp(newapp, building, 2); 
                            
                            await RegNewApp(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/2.3":

                            building = Program.RepositoryBuildings.FindItem(3);

                            newapp = Program.RepositoryApplications.FindItem(_clientStates[update.CallbackQuery.Message.Chat.Id].Value);

                            if (newapp != null)
                                Program.RepositoryApplications.UpdateBuildingApp(newapp, building, 2); 
                            
                            await RegNewApp(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/3":

                            building = Program.RepositoryBuildings.FindItem(4);

                            newapp = Program.RepositoryApplications.FindItem(_clientStates[update.CallbackQuery.Message.Chat.Id].Value);

                            if (newapp != null)
                                Program.RepositoryApplications.UpdateBuildingApp(newapp, building, 2); 
                            
                            await RegNewApp(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/5":

                            building = Program.RepositoryBuildings.FindItem(5);

                            newapp = Program.RepositoryApplications.FindItem(_clientStates[update.CallbackQuery.Message.Chat.Id].Value);

                            if (newapp != null)
                                Program.RepositoryApplications.UpdateBuildingApp(newapp, building, 2); 
                            
                            await RegNewApp(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/7":

                            building = Program.RepositoryBuildings.FindItem(6);

                            newapp = Program.RepositoryApplications.FindItem(_clientStates[update.CallbackQuery.Message.Chat.Id].Value);

                            if (newapp != null)
                                Program.RepositoryApplications.UpdateBuildingApp(newapp, building, 2); 
                            
                            await RegNewApp(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;



                    }

                    Console.WriteLine($"Данные из запроса: {update.CallbackQuery.Data}");
                    break;

                case UpdateType.Message:
                                       
                    var chatId = update.Message.Chat.Id;

                    ouremployee = Program.RepositoryEmployees.FindItemChatID(chatId);

                    if (ouremployee == null)
                    {

                        Program.RepositoryEmployees.AddNewEmployee(chatId);
                        ouremployee = Program.RepositoryEmployees.FindItemChatID(chatId);
                        _clientStates[chatId] = new UserStates { State = State.newemployee, Value = ouremployee.Id };

                    }

                    var statechat = _clientStates.ContainsKey(chatId) ? _clientStates[chatId] : _clientStates[chatId] = new UserStates { State = State.none, Value = 0 };


                    await HandleMessage(update, cancellationToken, botClient, statechat, ouremployee, chatId);

                    break;


            }


        }

        private async Task HandleMessage(Update update, CancellationToken cancellationToken, ITelegramBotClient botClient, UserStates statechat, Employee ouremployee, long chatId)
        {
            if (update.Type != UpdateType.Message)
                return;

            if (update.Message!.Type != MessageType.Text)
                return;

           var messageText = update.Message.Text;

            
            if (statechat.State != State.none)
            {
                switch (statechat.State)
                {
                    case State.newemployee:
                        await RegNewUser(ouremployee, botClient, cancellationToken, chatId, update);
                        break;

                    case State.newapp:

                        await RegNewApp(botClient, cancellationToken, chatId, update, ouremployee);
                        break;

                }

            }
            else
            {
                switch (messageText)

                {
                    case "/start":
                                                
                        if (ouremployee.State != 4)
                        {
                           await botClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: "Необходимо пройти регистрацию",
                                        cancellationToken: cancellationToken);

                            await RegNewUser(ouremployee, botClient, cancellationToken, chatId, update);  
                        }
                        
                        break;
                    case "Подать новую заявку":
                        {
                            
                            var newApp = Program.RepositoryApplications.AddNewApp(ouremployee);

                            Program.RepositoryApplicationActions.AddNewAppAction(newApp.Id, ouremployee.Id);
                            _clientStates[chatId] = new UserStates { State = State.newapp, Value = newApp.Id };

                            await RegNewApp(botClient, cancellationToken, chatId, update, ouremployee);

                            break;
                        }
                    case "Посмотреть неисполненные заявки":
                        {

                            var listapp = GetApplications.FindAll(chatId);

                            if (listapp.Count != 0)
                            {

                                foreach (var appaction in listapp)
                                {
                                    var app = Program.RepositoryApplications.FindItem(appaction.AppID);

                                    await botClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: "Заявка № - " + app.ToString() + ",\nсостояние - " + appaction.ApplicationState,
                                        cancellationToken: cancellationToken);

                                }

                                await botClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: "Для новой задачи воспользуйтесь меню!",
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

                            }
                            else
                            {
                                await botClient.SendTextMessageAsync(
                                            chatId: chatId,
                                            text: "Ваших неисполненных заявок нет",
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
                            }
                            break;
                        }
                    default:
                        await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Выберите задачу на панели!",
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
                        break;

                }
            }

        }

        private async Task RegNewApp(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId, Update update, Employee ouremployee)
        {
            var statewrite = Program.RepositoryApplications.FindItem(_clientStates[chatId].Value).statewrite;
            var messageText = "";

            if (update.Message != null)
            {
                messageText = update.Message.Text;
            }
            
            switch (statewrite)
            {
                case 0:
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
                case 1:
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

                    await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Введите номер кабинета",
                                cancellationToken: cancellationToken);
                    Program.RepositoryApplications.ChangeState(Program.RepositoryApplications.FindItem(_clientStates[chatId].Value), 3);
                    break;
                case 3:

                    Program.RepositoryApplications.UpdateRoomApp(Program.RepositoryApplications.FindItem(_clientStates[chatId].Value), messageText);

                    await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Введите контактный телефон",
                                cancellationToken: cancellationToken);
                    Program.RepositoryApplications.ChangeState(Program.RepositoryApplications.FindItem(_clientStates[chatId].Value), 4);
                    break;
                case 4:

                    Program.RepositoryApplications.UpdatePhoneApp(Program.RepositoryApplications.FindItem(_clientStates[chatId].Value), messageText);

                    await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Введите текст заявки",
                                cancellationToken: cancellationToken);
                    Program.RepositoryApplications.ChangeState(Program.RepositoryApplications.FindItem(_clientStates[chatId].Value), 5);
                    break;
                case 5:

                    Program.RepositoryApplications.UpdateContentApp(Program.RepositoryApplications.FindItem(_clientStates[chatId].Value), messageText);
                    Program.RepositoryApplications.ChangeState(Program.RepositoryApplications.FindItem(_clientStates[chatId].Value), 6);

                    //TODO создать запись в таблице движения заявок
                    Program.RepositoryApplicationActions.AddNewAppAction(_clientStates[chatId].Value, ouremployee.Id);

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

                    _clientStates[chatId] = new UserStates { State = State.none, Value = 0 };
                    break;


            }

        }

        private async Task RegNewUser(Employee ouremployee, ITelegramBotClient botClient, CancellationToken cancellationToken,
                                        long chatId, Update update)
        {
            if (ouremployee == null)
            {
                Program.RepositoryEmployees.AddNewEmployee(chatId);
                ouremployee = Program.RepositoryEmployees.FindItemChatID(chatId);
                _clientStates[update.Message.Chat.Id] = new UserStates { State = State.newemployee, Value = ouremployee.Id };
            }

            var stateuser = ouremployee.State;

            switch (stateuser)
            {
                case 0:
                    await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Для регистрации введите ФИО!",
                                cancellationToken: cancellationToken);
                    Program.RepositoryEmployees.ChangeState(ouremployee, 1);
                    break;

                case 1:
                    Program.RepositoryEmployees.UpdateFIOEmployee(chatId, update.Message.ToString());

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
                case 2:
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
                case 3:
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

