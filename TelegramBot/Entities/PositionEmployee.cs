using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    [Table(Name = "PositionEmployee")]
    public class PositionEmployee : BaseEntity
    {
        [Column(Name = "Name")]
        public string Name { get; set; }

        public PositionEmployee(int id,string name)
        { 
            this.ID = id;   
            this.Name = name;
        
        }

        public PositionEmployee() { }

        public override string ToString()
        {
            return Name;
        }



    }
}
