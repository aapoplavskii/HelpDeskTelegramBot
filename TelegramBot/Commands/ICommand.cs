﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot.Commands
{
    public interface ICommand
    {
        Task<Response> Execute(Update update);
    }
}
