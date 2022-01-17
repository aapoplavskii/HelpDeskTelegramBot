using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class Application : BaseEntity
    {
        public TypeApplication TypeApplication { get; set; }
        public Employee Employee { get; set; }
        public Building Building { get; set; }
        public string Room { get; set; }
        public string ContactTelephone { get; set; }
        public string Content { get; set; }
        public ApplicationState State { get; set; }
        public bool IsDelete { get; set; }

        private int index = 0;

        public DateTime DateCreateApp {get; set;}

        public int statewrite { get; set; }

        public Application(Employee employee)
        {

            Id = index++;
            Employee = employee;
            statewrite = 0;
            IsDelete = true;

        
        }


    }
}
