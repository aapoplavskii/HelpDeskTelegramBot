using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public interface IRepositoryAdditionalDatabases<T> where T : BaseEntity
    {
        public void AddItem(T item);

        public T FindItem(int id);

        public List<List<InlineKeyboardButton>> GetInlineKeyboardButtons();
    }
}
