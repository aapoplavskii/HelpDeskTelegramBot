using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

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
            //проинформировать пользователя
            
            return Task.CompletedTask;
        }

        private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;

            Console.WriteLine($"Пришло сообщение из чата {chatId} с текстом: '{messageText}'");
                        
            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Доброго дня, вывожу весь список актуальных заявок.",
                cancellationToken: cancellationToken);

            var dbcontent = GetEmployees.GetEmpolyee();

            foreach (var employee in dbcontent)
            {
                    sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: employee.Content,
                    cancellationToken: cancellationToken);

            }

            //Вывести список возможных команд

        }
    }
}
