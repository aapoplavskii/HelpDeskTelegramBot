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
    public class SubmitNewAppCommand : ICommand
    {
        private Employee _ouremployee;
        private Update _update;
        private CancellationToken _cancellationToken;
        private ITelegramBotClient _botClient;
        private RegNewUserCommand _regNewUserCommand;
        private IApplicationRepository _repositoryApplications;
        private IRepositoryEmployees _repositoryEmployees;
        private Dictionary<long, UserStates> _clientStates;
        private RegNewAppCommand _regNewAppCommand;

        public SubmitNewAppCommand(Employee employee, Update update, CancellationToken cancellationToken, ITelegramBotClient botClient, 
            IApplicationRepository repositoryApplications, IRepositoryEmployees repositoryEmployees,
            Dictionary<long, UserStates> clientStates, RegNewAppCommand regNewAppCommand)
        {
            _ouremployee = employee;
            _update = update;
            _cancellationToken = cancellationToken;
            _botClient = botClient;
            _repositoryApplications = repositoryApplications;
            _repositoryEmployees = repositoryEmployees;
            _clientStates = clientStates;
            _regNewAppCommand = regNewAppCommand;

        }


        public async Task<Response> Execute(Update update)
        {
            var chatId = update.Message.Chat.Id;
            
            var newApp = _repositoryApplications.AddNewApp(chatId, _repositoryEmployees);

            _clientStates[chatId] = new UserStates { State = State.newapp, Value = newApp.ID };

            await _regNewAppCommand.RegNewApp(_botClient, _cancellationToken, chatId, update, _ouremployee);

            return new Response { Message = "Новая заявка зарегистрирована." };
        }
    }
}
