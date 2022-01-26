using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class TakeCommand : ICommand
    {
        private Dictionary<long, UserStates> _clientStates;
        public TakeCommand(Dictionary<long, UserStates> clientStates)
        {
            if (_clientStates == null)
            { 
                throw new ArgumentNullException(nameof(clientStates));
            }

            _clientStates = clientStates;
        
        }

        public async Task<Response> Execute(Update update)
        {
            var chatId = update.Message.Chat.Id;

            _clientStates[chatId] = new UserStates { State = State.takeapp, Value = 0 };

            return new Response { Message = "Введите номер заявки" };
        }
    }

    public class Response
    { 
        public string Message { get; set; }
    }
}
