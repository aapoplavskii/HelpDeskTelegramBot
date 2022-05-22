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

        private Dictionary<long, UserStates> _clientStates = new();

        private IRepositoryEmployees _repositoryEmployees = new RepositoryEmployeeSQL();

        private IApplicationRepository _repositoryApplications = new ApplicationRepositorySQL();

        private IApplicationActionRepository _repositoryApplicationActions = new ApplicationActionRepositorySQL();

        private IRepositoryAdditionalDatabases<PositionEmployee> _repositoryPositions = new RepositoryAdditionalDatabasesSQL<PositionEmployee>();

        private IRepositoryAdditionalDatabases<Department> _repositoryDepartment = new RepositoryAdditionalDatabasesSQL<Department>();

        private IRepositoryAdditionalDatabases<Building> _repositoryBuildings = new RepositoryAdditionalDatabasesSQL<Building>();

        private IRepositoryAdditionalDatabases<TypeApplication> _repositoryTypeApplication = new RepositoryAdditionalDatabasesSQL<TypeApplication>();

        private IRepositoryAdditionalDatabases<ApplicationState> _repositoryApplicationState = new RepositoryAdditionalDatabasesSQL<ApplicationState>();


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
            Employee ouremployee;
            
            ICommand command;

            Response response;
                      

            RegNewAppCommand regNewAppCommand = new RegNewAppCommand(_repositoryApplications, _repositoryApplicationActions, _clientStates, _repositoryBuildings, _repositoryEmployees,
                _repositoryDepartment);

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


                    switch (update.CallbackQuery.Data)
                    {


                        case "/медицина":

                            Command.UpdatePositionUser(1, _repositoryPositions, _repositoryEmployees, update, cancellationToken, botClient, ouremployee, _repositoryDepartment, _clientStates);

                            break;
                        case "/общие":

                            Command.UpdatePositionUser(2, _repositoryPositions, _repositoryEmployees, update, cancellationToken, botClient, ouremployee, _repositoryDepartment, _clientStates);

                            break;
                        case "/наука":

                            Command.UpdatePositionUser(3, _repositoryPositions, _repositoryEmployees, update, cancellationToken, botClient, ouremployee, _repositoryDepartment, _clientStates);

                            break;
                        case "/инженер":

                            Command.UpdatePositionUser(4, _repositoryPositions, _repositoryEmployees, update, cancellationToken, botClient, ouremployee, _repositoryDepartment, _clientStates);

                            break;

                        case "/администрация":

                            Command.UpdateDepartmentUser(1, _repositoryDepartment, _repositoryEmployees, update, cancellationToken, botClient, ouremployee, _repositoryPositions,_clientStates);

                            break;
                        case "/поликлиника":

                            Command.UpdateDepartmentUser(2, _repositoryDepartment, _repositoryEmployees, update, cancellationToken, botClient, ouremployee, _repositoryPositions, _clientStates);

                            break;
                        case "/клиника":

                            Command.UpdateDepartmentUser(3, _repositoryDepartment, _repositoryEmployees, update, cancellationToken, botClient, ouremployee, _repositoryPositions, _clientStates);

                            break;
                        case "/наука_отдел":

                            Command.UpdateDepartmentUser(4, _repositoryDepartment, _repositoryEmployees, update, cancellationToken, botClient, ouremployee, _repositoryPositions, _clientStates);

                            break;
                        case "/диагностика":

                            Command.UpdateDepartmentUser(5, _repositoryDepartment, _repositoryEmployees, update, cancellationToken, botClient, ouremployee, _repositoryPositions, _clientStates);

                            break;
                        case "/кафедра":

                            Command.UpdateDepartmentUser(6, _repositoryDepartment, _repositoryEmployees, update, cancellationToken, botClient, ouremployee, _repositoryPositions, _clientStates);

                            break;
                        case "/да":

                            Command.UpdateExecutorEmployee(_repositoryEmployees, update, cancellationToken, botClient, ouremployee, true, _repositoryPositions,_clientStates,_repositoryDepartment);

                            break;
                        case "/нет":

                            Command.UpdateExecutorEmployee(_repositoryEmployees, update, cancellationToken, botClient, ouremployee, true, _repositoryPositions, _clientStates, _repositoryDepartment);

                            break;
                        case "/ремонт":

                            Command.UpdateTypeApp(_repositoryTypeApplication, _repositoryApplications, update, botClient, cancellationToken, _clientStates, ouremployee,
                                regNewAppCommand, 1);

                            break;
                        case "/сеть":

                            Command.UpdateTypeApp(_repositoryTypeApplication, _repositoryApplications, update, botClient, cancellationToken, _clientStates, ouremployee,
                                regNewAppCommand, 2);

                            break;
                        case "/МИС":

                            Command.UpdateTypeApp(_repositoryTypeApplication, _repositoryApplications, update, botClient, cancellationToken, _clientStates, ouremployee,
                                regNewAppCommand, 3);

                            break;
                        case "/прочее":

                            Command.UpdateTypeApp(_repositoryTypeApplication, _repositoryApplications, update, botClient, cancellationToken, _clientStates, ouremployee,
                                regNewAppCommand, 4);

                            break;

                        case "/2.1":

                            Command.UpdateBuildingApp(_repositoryBuildings, _repositoryApplications, update, botClient, cancellationToken, _clientStates, ouremployee,
                                regNewAppCommand, 1);

                            break;
                        case "/2.2":

                            Command.UpdateBuildingApp(_repositoryBuildings, _repositoryApplications, update, botClient, cancellationToken, _clientStates, ouremployee,
                                regNewAppCommand, 2);

                            break;
                        case "/2.3":

                            Command.UpdateBuildingApp(_repositoryBuildings, _repositoryApplications, update, botClient, cancellationToken, _clientStates, ouremployee,
                                regNewAppCommand, 3);

                            break;
                        case "/3":

                            Command.UpdateBuildingApp(_repositoryBuildings, _repositoryApplications, update, botClient, cancellationToken, _clientStates, ouremployee,
                                regNewAppCommand, 4);

                            break;
                        case "/5":

                            Command.UpdateBuildingApp(_repositoryBuildings, _repositoryApplications, update, botClient, cancellationToken, _clientStates, ouremployee,
                                regNewAppCommand, 5);

                            break;
                        case "/7":

                            Command.UpdateBuildingApp(_repositoryBuildings, _repositoryApplications, update, botClient, cancellationToken, _clientStates, ouremployee,
                                 regNewAppCommand, 6);

                            break;



                    }

                    break;

                case UpdateType.Message:

                    if (update.Type != UpdateType.Message)
                        return;

                    if (update.Message!.Type != MessageType.Text)
                        return;

                    var chatId = update.Message.Chat.Id;

                    ouremployee = _repositoryEmployees.FindItemChatID(chatId);

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

                                command = new RegNewUserCommand(_repositoryEmployees,_repositoryPositions, _repositoryDepartment,_clientStates,
                                    botClient, cancellationToken, update, ouremployee);
                                await command.Execute(update);
                                
                                break;

                            case State.newapp:

                                await regNewAppCommand.RegNewApp(botClient, cancellationToken, chatId, update, ouremployee);
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
                                command = new RegNewUserCommand(_repositoryEmployees, _repositoryPositions, _repositoryDepartment, _clientStates,
                                    botClient, cancellationToken, update, ouremployee);
                                
                                await command.Execute(update);

                                break;
                            case "Подать новую заявку":
                                {
                                    if (ouremployee.State != 4)
                                    {
                                        await botClient.SendTextMessageAsync(
                                                     chatId: chatId,
                                                     text: "Необходимо пройти регистрацию",
                                                     cancellationToken: cancellationToken);

                                        command = new RegNewUserCommand(_repositoryEmployees, _repositoryPositions, _repositoryDepartment, _clientStates,
                                        botClient, cancellationToken, update, ouremployee);

                                        await command.Execute(update);
                                    }

                                    command = new SubmitNewAppCommand(ouremployee, update, cancellationToken, botClient, _repositoryApplications,
                                        _repositoryEmployees, _clientStates, regNewAppCommand);
                                    response = await command.Execute(update);

                                    await botClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: response.Message,
                                        cancellationToken: cancellationToken);

                                    break;
                                }
                            case "Посмотреть неисполненные заявки":
                                {
                                    command = new TakeAllPendingApp(_repositoryApplications, _repositoryTypeApplication, _repositoryEmployees,
                                        _repositoryApplicationState, cancellationToken, botClient, _clientStates);
                                    response = await command.Execute(update);

                                    await botClient.SendTextMessageAsync(
                                    chatId: chatId,
                                    text: response.Message,
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

                    break;


            }


        }


    }
}

