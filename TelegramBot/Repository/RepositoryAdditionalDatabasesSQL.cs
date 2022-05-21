using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public class RepositoryAdditionalDatabasesSQL<T> : IRepositoryAdditionalDatabases<T> where T : BaseEntity
    {
        public void AddItem(T item)
        {
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                var table = db.Insert(item);
            }
        }

        public T FindItem(int id)
        {
            T item = null;

            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                item = db.GetTable<T>().FirstOrDefault(x => x.ID == id);
            }

            return item;

        }
                
        public List<List<InlineKeyboardButton>> GetInlineKeyboardButtons()
        {
            var result = new List<List<InlineKeyboardButton>>();

            var buttons = new List<T>();

            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                buttons = db.GetTable<T>().Select(x => x).ToList();
            }
                        
            var rowcount = (int)Math.Ceiling(buttons.Count() / 2f);
            
            for (int i = 0; i < rowcount; i++)
            {
                var list = new List<InlineKeyboardButton>();

                list.AddRange(buttons.Skip(i*2).Take(2).Select(i => InlineKeyboardButton.WithCallbackData(i.Name, i.Name)));

                result.Add(list);
            }

            return result;

        }

    }
}
