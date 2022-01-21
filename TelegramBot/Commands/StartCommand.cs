using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Commands
{
    public class StartCommand : ICommand
    {
        ITelegramBotClient BotClient = null;
        IRepositoryEmployees _repository = null;

        public StartCommand(IRepositoryEmployees repository, ITelegramBotClient botClient)
        {
            _repository = repository;
            BotClient = botClient;
        }        

        public async Task<Response> Execute(Update update)
        {

            // Создать запись нового пользователя
            // Установить стейт пользователя
            // Запросили в базе кнопки для стейта
            // var actions = создали нужный массив кнопок из того, что пришло из базы
            return new Response { Message = "Введите ФИО", Actions = new List<ResponseAction>() { } };
        }
    }

    public class Response
    {
        public string Message {get;set;}

        public List<ResponseAction> Actions {get;set;}
    }

    public class ResponseAction
    {
        public string Text {get;set;}
        public string Payload {get;set;}
    }
}
