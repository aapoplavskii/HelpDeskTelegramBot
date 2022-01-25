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
        public async Task<Response> Execute(Update update)
        {


            return new Response { Message = "Введите номер заявки" };
        }
    }

    public class Response
    { 
        public string Message { get; set; }
    }
}
