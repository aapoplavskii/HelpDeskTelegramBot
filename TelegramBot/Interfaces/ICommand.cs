using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public interface ICommand
    {
        Task<Response> Execute(Update update);

    }
}
