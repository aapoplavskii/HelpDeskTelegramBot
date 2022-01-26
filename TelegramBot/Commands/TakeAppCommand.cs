using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class TakeAppCommand: ICommand
    {
        private Dictionary<long, UserStates> _clientStates;
        private int _employeeID;
        public TakeAppCommand(Dictionary<long, UserStates> clientStates, int employeeID)
        {
            if (clientStates == null)
            {
                throw new ArgumentNullException(nameof(clientStates));
            }

            _clientStates = clientStates;
            
            //TODO: проверка на null
            _employeeID = employeeID;

        }

        public Task<Response> Execute(Update update)
        {
            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;

            if (int.TryParse(messageText, out int appID))
            {
                Program.RepositoryApplicationActions.AddNewAppAction(appID, _employeeID, 2);  //TODO: передать через конструктор

                _clientStates[chatId] = new UserStates { State = State.none, Value = 0 };

                return Task.FromResult(new Response { Message = $"Заявка № {appID} взята в работу!" });
            }
            else
            { 
            _clientStates[chatId] = new UserStates { State = State.takeapp, Value = 0 };

            return Task.FromResult(new Response { Message = "Введите номер заявки цифрами!" });
            }


            
        }
    }
//TODO: вынести в базовый класс повторяющийся код
}
