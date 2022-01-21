using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Commands;

namespace TelegramBot.Services
{
    public class CommandFactory
    {
        Dictionary<int, TelegramBot.Commands.ICommand> _commandDictionary = new Dictionary<int, TelegramBot.Commands.ICommand>()
        {
            { 1, new TelegramBot.Commands.NewTaskCommand() },
            { 2,  }
        };

        public TelegramBot.Commands.ICommand GetCommand(Update update)
        {
            TelegramBot.Commands.ICommand command = null;

            // Запросить состояние пользователя из базы
            var state = 1;

            command = _commandDictionary[state];

            return command;
            //var messageText = update.Message.Text;

            //switch (messageText)

            //{
            //    case "/start":
            //        {
            //            command = new TelegramBot.Commands.StartCommand();
            //            break;
            //        }
            //    case "Подать новую заявку":
            //        {
            //            command = new TelegramBot.Commands.StartCommand();
            //            break;
            //        }
            //}

            //return command;
        }
    }
}
