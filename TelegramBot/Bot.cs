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

        private Dictionary<long, UserState> _clientstate = new Dictionary<long, UserState>();

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

            switch (update.Type)
            {

                case UpdateType.CallbackQuery:
                    // update.CallbackQuery.Data - это то, что было указано в callbackData кнопки
                    switch (update.CallbackQuery.Data)
                    {
                        case "медицина":

                            var position = Program.RepositoryPositions.FindItem(1);
                            
                            Program.RepositoryEmployees.UpdatePositionEmployee(update.CallbackQuery.Message.Chat.Id, 3, position);
                            
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

            switch (messageText)

            {

                case "/start":
                    {

                        if (ouremployee == null)
                        {

                            Program.RepositoryEmployees.AddNewEmployee(chatId);

                            Message sentMessage = await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Вы не идентифицированы! Для регистрации введите ФИО!",
                                cancellationToken: cancellationToken);
                        }
                        else
                        {
                            GetStateUser(ouremployee, botClient, cancellationToken, chatId, Program.RepositoryEmployees, update);

                        }
                        break;
                    }
                case "/new":
                    {

                        Command.SubmitNewApplication();

                        break;
                    }





            }



        }

        private async void GetStateUser(Employee ouremployee, ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId, RepositoryEmployees repositoryEmployees, Update update)
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
                        InlineKeyboardButton.WithCallbackData("Сотрудник администрации", "/администрация")
                    },
                    new List<InlineKeyboardButton>() {
                        InlineKeyboardButton.WithCallbackData("Научный сотрудник", "/наука"),
                        InlineKeyboardButton.WithCallbackData("Медицинский инженер", "/инженер")
                    }
                }));
                    repositoryEmployees.ChangeState(ouremployee, 2);
                    break;
                case 3:
                    await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Выберите подразделение",
                                cancellationToken: cancellationToken);
                    repositoryEmployees.ChangeState(ouremployee, 3);
                    break;

                default:
                    break;

            }
        }
    }
}

