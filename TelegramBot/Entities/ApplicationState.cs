using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    [Table(Name = "ApplicationState")]
    public class ApplicationState: BaseEntity
    {
        [Column(Name = "Name")]
        public string Name { get; set; }

        public ApplicationState(int id, string name)
        { 
            this.ID = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
