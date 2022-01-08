using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class Department: BaseEntity
    {
        public string Name { get; set; }

        public Department(int id, string name)
        { 
            this.Name = name;
            this.Id = id;
        
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
