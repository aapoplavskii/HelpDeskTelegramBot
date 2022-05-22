using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Commands
{
    public class TakeAllPendingApp : ICommand
    {
        private IApplicationRepository _repositoryApplications;
        private IRepositoryAdditionalDatabases<TypeApplication> _repositoryTypeApplication;
        private IRepositoryEmployees _repositoryEmployees;
        private IRepositoryAdditionalDatabases<ApplicationState> _repositoryApplicationState;
        private CancellationToken _cancellationToken;
        private ITelegramBotClient _botClient;
        private Dictionary<long, UserStates> _clientStates;

        public TakeAllPendingApp(IApplicationRepository repositoryApplications, IRepositoryAdditionalDatabases<TypeApplication> repositoryTypeApplication,
            IRepositoryEmployees repositoryEmployees, IRepositoryAdditionalDatabases<ApplicationState> repositoryApplicationState, CancellationToken cancellationToken,
            ITelegramBotClient botClient, Dictionary<long, UserStates> clientStates)
        {
            _repositoryApplications = repositoryApplications;
            _repositoryTypeApplication = repositoryTypeApplication;
            _repositoryEmployees = repositoryEmployees;
            _repositoryApplicationState = repositoryApplicationState;
            _botClient = botClient;
            _cancellationToken = cancellationToken;
            _clientStates = clientStates;
        }

        public async Task<Response> Execute(Update update)
        {
            var chatId = update.Message.Chat.Id;

            var listmessage = GetApplicationsSQL.FindAll(chatId, _repositoryApplications, _repositoryTypeApplication,
                _repositoryEmployees, _repositoryApplicationState);

            if (listmessage.Count != 0)
            {

                foreach (var message in listmessage)
                {

                    await _botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: message,
                        cancellationToken: _cancellationToken);

                }

                _clientStates[chatId] = new UserStates { State = State.none, Value = 0 };

                return new Response { Message = "Для новой задачи воспользуйтесь меню!" };
           
            }
            else
            {
                _clientStates[chatId] = new UserStates { State = State.none, Value = 0 };

                return new Response { Message = "Ваших неисполненных заявок нет" };
              
            }

             
        }
    }
}
