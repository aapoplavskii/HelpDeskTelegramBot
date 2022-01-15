using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class TypeApplication: BaseEntity
    {
        public string Name { get; set; }

        public TypeApplication(int id, string name)
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
