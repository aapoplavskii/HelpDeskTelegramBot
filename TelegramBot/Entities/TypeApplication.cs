using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    [Table(Name = "TypeApplication")]
    public class TypeApplication: BaseEntity
    {
        [Column(Name = "Name")]
        public string Name { get; set; }

        public TypeApplication(int id, string name)
        {
            this.Name = name;
            this.ID = id;   
            
        }

        public TypeApplication() { }

        public override string ToString()
        { 
            return Name;
        }

       
    }
}
