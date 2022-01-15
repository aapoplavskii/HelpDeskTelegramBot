using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Tsourse FindItem<Tsourse>(int id); 

        public Employee FindFIOSotr(string sotr);
    
    }
}
