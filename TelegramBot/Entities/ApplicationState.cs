using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class ApplicationState: BaseEntity
    {
        public string Name { get; set; }

        public ApplicationState(int id, string name)
        { 
            this.Id = id;
            this.Name = name;
        }

    }
}
