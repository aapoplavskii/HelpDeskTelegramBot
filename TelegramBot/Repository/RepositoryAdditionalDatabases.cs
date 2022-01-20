using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class RepositoryAdditionalDatabases<T>: IRepositoryAdditionalDatabases<T> where T : BaseEntity
    {
   
        private List<T> _listitems = new List<T>();

        public void AddItem(T item)
        { 
            _listitems.Add(item);   
        
        }

        public T FindItem(int id) => _listitems.FirstOrDefault(s => s.Id == id);

    }
}
