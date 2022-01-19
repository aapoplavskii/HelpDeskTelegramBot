using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public interface IRepositoryAdditionalDatabases<T> where T : BaseEntity
    {
        public void AddItem(T item);

        public T FindItem(int id);

    }
}
