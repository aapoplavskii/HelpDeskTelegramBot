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
    public static class Bot
    {
        private static TelegramBotClient bot;

        public static void InitBot()
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
        }


        private static Task HandleErrorAsync(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            var ErrorMessage = arg2 switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => arg2.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message)
                return;

            if (update.Message!.Type != MessageType.Text)
                return;


            var chatId = update.Message.Chat.Id;

            var messageText = update.Message.Text;

            var user = update.Message.Chat.LastName;

            switch (messageText)

            {
                case "/start":
                    {
                        var ouremployee = Command.InitUser(user);

                        if (ouremployee != null)
                        {
                            Message sentMessage = await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: $"Вы зарегистрированы под пользователем - {ouremployee}",
                                cancellationToken: cancellationToken);
                            Console.WriteLine($"Вы зарегистрированы под пользователем - {ouremployee}");
                        }
                        else
                        {
                            Message sentMessage = await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Вы не идентифицированы! Для регистрации введите необходимые данные!",
                                cancellationToken: cancellationToken);
                            Console.WriteLine("Вы не идентифицированы! Для регистрации введите необходимые данные!");

                            if (messageText == "Вы не идентифицированы! Для регистрации введите необходимые данные!")
                            {
                                await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Введите ФИО !",
                                cancellationToken: cancellationToken);

                            }

                            if (messageText == "Введите ФИО !")
                            {
                                await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Введите должность",
                                cancellationToken: cancellationToken, replyMarkup: GetButtonsRegistrationUser());

                            }

                            Command.SubmitNewUser();

                        }
                        break;
                    }
                case "/new":
                    {

                        Command.SubmitNewApplication();

                        break;
                    }
                




            }

            //Console.WriteLine($"Пришло сообщение из чата {chatId} с текстом: '{messageText}'");




            //Message sentMessage = await botClient.SendTextMessageAsync(
            //    chatId: chatId,
            //    text: "Доброго дня, вывожу весь список актуальных заявок.",
            //    cancellationToken: cancellationToken);

            //var dbcontent = GetApplications.GetApplication();

            //foreach (var application in dbcontent)
            //{
            //        sentMessage = await botClient.SendTextMessageAsync(
            //        chatId: chatId,
            //        text: application.Content,
            //        cancellationToken: cancellationToken);

            //}

            //Вывести список возможных команд

        }

        private static IReplyMarkup GetStartWriteNewUserData(string name)
        {

            return new InlineKeyboardMarkup (InlineKeyboardButton.WithCallbackData("Начать ввод данных", $"name|{name}"));

        }

        private static IReplyMarkup GetButtonsRegistrationUser()
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                {
                new KeyboardButton[] { "Зарегистрировать нового пользователя" }

            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true,
            };

            return replyKeyboardMarkup;

        }

       

    }
}  
    
