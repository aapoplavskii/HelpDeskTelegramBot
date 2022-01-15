using System;

namespace TelegramBot
{
    public static class ChatBotSettings
    {
        public static string token { get; set; } = Environment.GetEnvironmentVariable("Telegram_bot_key");
           

    }
}
