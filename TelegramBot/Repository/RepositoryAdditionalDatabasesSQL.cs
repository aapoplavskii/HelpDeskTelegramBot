using LinqToDB;
using System.Linq;

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
    }
}
