using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class TakeAppCommand : ICommand
    {
        private Dictionary<long, UserStates> _clientStates = null;
        
        private int _employeeID = 0;

        private IApplicationActionRepository _applicationActionRepository = null;
        public TakeAppCommand(Dictionary<long, UserStates> clientStates, int employeeID, IApplicationActionRepository applicationActionRepository)
        {
            if (clientStates == null)
            {
                throw new ArgumentNullException(nameof(clientStates));
            }

            _clientStates = clientStates;
                        
            _employeeID = employeeID;
            
            if (applicationActionRepository == null)
            {
                throw new ArgumentNullException(nameof(applicationActionRepository));
            }
            _applicationActionRepository = applicationActionRepository;
        }

        public Task<Response> Execute(Update update)
        {
            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;

            if (int.TryParse(messageText, out int appID))
            {
                _applicationActionRepository.AddNewAppAction(appID, _employeeID, 2);

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
