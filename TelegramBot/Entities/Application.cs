using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class Application: BaseEntity
    {
        public TypeApplication TypeApplication { get; set; }
        public Employee Employee { get; set; }
        public Building Building { get; set; }
        public string Room { get; set; }      
        public string ContactTelephone { get; set; }       
        public string Content { get; set; }
        public ApplicationState state { get; set; }
        public bool IsDelete { get; set; }

        private int index = 0;

        public Application(TypeApplication typeApplication, Employee employee, Building building,
                            string room, string phone, string content, ApplicationState applicationState, bool isdelete)
        {

            index++; 

            this.Id = index;
            this.TypeApplication = typeApplication; 
            this.Employee = employee;
            this.Building = building;
            this.Room = room;
            this.ContactTelephone = phone;
            this.Content = content;
            this.state = applicationState;
            this.IsDelete = isdelete;
        
        }


    }
}
