using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class RepositoryAdditionalDatabasesSQL<T> : IRepositoryAdditionalDatabases<T> where T : BaseEntity
    {
        public void AddItem(T item)
        {
            using (Config.db)
            {
                var table = Config.db.Insert(item);
            }
        }

        public T FindItem(int id)
        {
            T item = null;

            using (Config.db)
            {
                item = Config.db.GetTable<T>().FirstOrDefault(x => x.Id == id);
            }

            return item;

        }
    }
}
