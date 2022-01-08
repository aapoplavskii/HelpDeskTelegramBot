using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class PositionEmployee : BaseEntity
    {
       public string Name { get; set; }

        public PositionEmployee(int id,string name)
        { 
            this.Id = id;   
            this.Name = name;
        
        }

        public override string ToString()
        {
            return Name;
        }



    }
}
