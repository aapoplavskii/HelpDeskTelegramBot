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
using TelegramBot.Commands;

namespace TelegramBot
{
    public class Bot
    {
        private TelegramBotClient bot;

        private Dictionary<long, UserStates> _clientStates = new Dictionary<long, UserStates>();

        public static IRepositoryEmployees _repositoryEmployees = new RepositoryEmployeeSQL();

        public static IApplicationRepository _repositoryApplications = new ApplicationRepositorySQL();

        public static IApplicationActionRepository _repositoryApplicationActions = new ApplicationActionRepositorySQL();

        public static IRepositoryAdditionalDatabases<PositionEmployee> _repositoryPositions = new RepositoryAdditionalDatabasesSQL<PositionEmployee>();

        public static IRepositoryAdditionalDatabases<Department> _repositoryDepartment = new RepositoryAdditionalDatabasesSQL<Department>();

        public static IRepositoryAdditionalDatabases<Building> _repositoryBuildings = new RepositoryAdditionalDatabasesSQL<Building>();

        public static IRepositoryAdditionalDatabases<TypeApplication> _repositoryTypeApplication = new RepositoryAdditionalDatabasesSQL<TypeApplication>();

        public static IRepositoryAdditionalDatabases<ApplicationState> _repositoryApplicationState = new RepositoryAdditionalDatabasesSQL<ApplicationState>();
              

        public void InitBot()
        {
            bot = new TelegramBotClient(ChatBotSettings.token);

            using var cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new UpdateType[] {
                    UpdateType.Message,
                    UpdateType.CallbackQuery
                    }
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

            RegNewUserCommand regNewUserCommand = new RegNewUserCommand(_repositoryEmployees,_repositoryPositions,_repositoryDepartment,_clientStates);

            RegNewAppCommand regNewAppCommand = new RegNewAppCommand(_repositoryApplications, _repositoryApplicationActions, _clientStates);

            switch (update.Type)
            {

                case UpdateType.CallbackQuery:

                    ouremployee = _repositoryEmployees.FindItemChatID(update.CallbackQuery.Message.Chat.Id);

                    if (ouremployee == null)
                    {

                        _repositoryEmployees.AddNewEmployee(update.CallbackQuery.Message.Chat.Id);
                        ouremployee = _repositoryEmployees.FindItemChatID(update.CallbackQuery.Message.Chat.Id);
                        _clientStates[update.CallbackQuery.Message.Chat.Id] = new UserStates { State = State.newemployee, Value = ouremployee.ID };

                    }

                    //TODO: вынести обработку ответов в отдельные команды исходя из состояния чата

                    switch (update.CallbackQuery.Data)
                    {


                        case "/медицина":

                            position = _repositoryPositions.FindItem(1);

                            _repositoryEmployees.UpdatePositionEmployee(update.CallbackQuery.Message.Chat.Id, position);
                            await regNewUserCommand.RegNewUser(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/общие":

                            position = _repositoryPositions.FindItem(2);

                            _repositoryEmployees.UpdatePositionEmployee(update.CallbackQuery.Message.Chat.Id, position);
                            await regNewUserCommand.RegNewUser(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/наука":

                            position = _repositoryPositions.FindItem(3);

                            _repositoryEmployees.UpdatePositionEmployee(update.CallbackQuery.Message.Chat.Id, position);
                            await regNewUserCommand.RegNewUser(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/инженер":

                            position = _repositoryPositions.FindItem(4);

                            _repositoryEmployees.UpdatePositionEmployee(update.CallbackQuery.Message.Chat.Id, position);
                            await regNewUserCommand.RegNewUser(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;

                        case "/администрация":

                            department = _repositoryDepartment.FindItem(1);

                            _repositoryEmployees.UpdateDepartmentEmployee(update.CallbackQuery.Message.Chat.Id, department);
                            await regNewUserCommand.RegNewUser(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/поликлиника":

                            department = _repositoryDepartment.FindItem(2);

                            _repositoryEmployees.UpdateDepartmentEmployee(update.CallbackQuery.Message.Chat.Id, department);
                            await regNewUserCommand.RegNewUser(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/клиника":

                            department = _repositoryDepartment.FindItem(3);

                            _repositoryEmployees.UpdateDepartmentEmployee(update.CallbackQuery.Message.Chat.Id, department);
                            await regNewUserCommand.RegNewUser(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/наука_отдел":

                            department = _repositoryDepartment.FindItem(4);

                            _repositoryEmployees.UpdateDepartmentEmployee(update.CallbackQuery.Message.Chat.Id, department);
                            await regNewUserCommand.RegNewUser(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/диагностика":

                            department = _repositoryDepartment.FindItem(5);

                            _repositoryEmployees.UpdateDepartmentEmployee(update.CallbackQuery.Message.Chat.Id, department);
                            await regNewUserCommand.RegNewUser(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/кафедра":

                            department = _repositoryDepartment.FindItem(6);

                            _repositoryEmployees.UpdateDepartmentEmployee(update.CallbackQuery.Message.Chat.Id, department);
                            await regNewUserCommand.RegNewUser(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);
                            break;
                        case "/да":

                            _repositoryEmployees.UpdateIsExecutorEmployee(update.CallbackQuery.Message.Chat.Id, true);
                            await regNewUserCommand.RegNewUser(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/нет":

                            _repositoryEmployees.UpdateIsExecutorEmployee(update.CallbackQuery.Message.Chat.Id, false);
                            await regNewUserCommand.RegNewUser(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/ремонт":

                            typeApplication = _repositoryTypeApplication.FindItem(1);

                            newapp = _repositoryApplications.FindItem(_clientStates[update.CallbackQuery.Message.Chat.Id].Value);

                            if (newapp != null)
                                _repositoryApplications.UpdateTypeApp(newapp.ID, typeApplication);

                            await regNewAppCommand.RegNewApp(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/сеть":

                            typeApplication = _repositoryTypeApplication.FindItem(2);

                            newapp = _repositoryApplications.FindItem(_clientStates[update.CallbackQuery.Message.Chat.Id].Value);

                            if (newapp != null)
                                _repositoryApplications.UpdateTypeApp(newapp.ID, typeApplication);

                            await regNewAppCommand.RegNewApp(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/МИС":

                            typeApplication = _repositoryTypeApplication.FindItem(3);

                            newapp = _repositoryApplications.FindItem(_clientStates[update.CallbackQuery.Message.Chat.Id].Value);

                            if (newapp != null)
                                _repositoryApplications.UpdateTypeApp(newapp.ID, typeApplication);

                            await regNewAppCommand.RegNewApp(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/прочее":

                            typeApplication = _repositoryTypeApplication.FindItem(4);

                            newapp = _repositoryApplications.FindItem(_clientStates[update.CallbackQuery.Message.Chat.Id].Value);

                            if (newapp != null)
                                _repositoryApplications.UpdateTypeApp(newapp.ID, typeApplication);

                            await regNewAppCommand.RegNewApp(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;

                        case "/2.1":

                            building = _repositoryBuildings.FindItem(1);

                            newapp = _repositoryApplications.FindItem(_clientStates[update.CallbackQuery.Message.Chat.Id].Value);

                            if (newapp != null)
                                _repositoryApplications.UpdateBuildingApp(newapp.ID, building);

                            await regNewAppCommand.RegNewApp(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/2.2":

                            building = _repositoryBuildings.FindItem(2);

                            newapp = _repositoryApplications.FindItem(_clientStates[update.CallbackQuery.Message.Chat.Id].Value);

                            if (newapp != null)
                                _repositoryApplications.UpdateBuildingApp(newapp.ID, building);

                            await regNewAppCommand.RegNewApp(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/2.3":

                            building = _repositoryBuildings.FindItem(3);

                            newapp = _repositoryApplications.FindItem(_clientStates[update.CallbackQuery.Message.Chat.Id].Value);

                            if (newapp != null)
                                _repositoryApplications.UpdateBuildingApp(newapp.ID, building);

                            await regNewAppCommand.RegNewApp(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/3":

                            building = _repositoryBuildings.FindItem(4);

                            newapp = _repositoryApplications.FindItem(_clientStates[update.CallbackQuery.Message.Chat.Id].Value);

                            if (newapp != null)
                                _repositoryApplications.UpdateBuildingApp(newapp.ID, building);

                            await regNewAppCommand.RegNewApp(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/5":

                            building = _repositoryBuildings.FindItem(5);

                            newapp = _repositoryApplications.FindItem(_clientStates[update.CallbackQuery.Message.Chat.Id].Value);

                            if (newapp != null)
                                _repositoryApplications.UpdateBuildingApp(newapp.ID, building);

                            await regNewAppCommand.RegNewApp(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

                            break;
                        case "/7":

                            building = _repositoryBuildings.FindItem(6);

                            newapp = _repositoryApplications.FindItem(_clientStates[update.CallbackQuery.Message.Chat.Id].Value);

                            if (newapp != null)
                                _repositoryApplications.UpdateBuildingApp(newapp.ID, building);

                            await regNewAppCommand.RegNewApp(botClient, cancellationToken, update.CallbackQuery.Message.Chat.Id, update, ouremployee);

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
            ICommand command = null;
            Response response = null;
            RegNewUserCommand regNewUserCommand = new RegNewUserCommand(_repositoryEmployees, _repositoryPositions, _repositoryDepartment, _clientStates);


            if (update.Type != UpdateType.Message)
                return;

            if (update.Message!.Type != MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;

            var ouremployee = _repositoryEmployees.FindItemChatID(chatId);

            if (ouremployee == null)
            {

                _repositoryEmployees.AddNewEmployee(chatId);
                ouremployee = _repositoryEmployees.FindItemChatID(chatId);
                _clientStates[chatId] = new UserStates { State = State.newemployee, Value = ouremployee.ID };

            }

            var statechat = _clientStates.ContainsKey(chatId) ? _clientStates[chatId] : _clientStates[chatId] = new UserStates { State = State.none, Value = 0 };

            var messageText = update.Message.Text;

            //TODO: вынести все команды в отдельные классы
            if (statechat.State != State.none)
            {
                switch (statechat.State)
                {
                    case State.newemployee:
                        await regNewUserCommand.RegNewUser(botClient, cancellationToken, chatId, update, ouremployee);
                        break;

                    case State.newapp:

                        await RegNewApp(botClient, cancellationToken, chatId, update, ouremployee);
                        break;

                    case State.takeapp:

                        command = new TakeAppCommand(_clientStates, ouremployee.ID, _repositoryApplicationActions);
                        response = await command.Execute(update);


                        await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: response.Message,
                                cancellationToken: cancellationToken);

                        break;

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
                        }
                        await regNewUserCommand.RegNewUser(botClient, cancellationToken, chatId, update, ouremployee);

                        break;
                    case "Подать новую заявку":
                        {

                            if (ouremployee.State != 4)
                            {
                                await botClient.SendTextMessageAsync(
                                             chatId: chatId,
                                             text: "Необходимо пройти регистрацию",
                                             cancellationToken: cancellationToken);
                                await regNewUserCommand.RegNewUser(botClient, cancellationToken, chatId, update, ouremployee);
                            }

                            var newApp = _repositoryApplications.AddNewApp(chatId);

                            _clientStates[chatId] = new UserStates { State = State.newapp, Value = newApp.ID };

                            await RegNewApp(botClient, cancellationToken, chatId, update, ouremployee);

                            break;
                        }
                    case "Посмотреть неисполненные заявки":
                        {

                            var listmessage = GetApplicationsSQL.FindAll(chatId);

                            if (listmessage.Count != 0)
                            {

                                foreach (var message in listmessage)
                                {

                                    await botClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: message,
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
                    case "/take":
                        {
                            command = new TakeCommand(_clientStates);
                            response = await command.Execute(update);

                            await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: response.Message,
                                cancellationToken: cancellationToken);

                            break;
                        }



                }
            }

        }

        

        
    }
}

