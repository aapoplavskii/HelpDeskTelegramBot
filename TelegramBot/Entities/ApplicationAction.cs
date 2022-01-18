using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class ApplicationAction: BaseEntity
    {
        public int AppID { get; set; }
        public int EmployeeID { get; set; } 
        public int EmployeeIDEx { get; set; }
        public ApplicationState ApplicationState { get; set; }
        public int AppState { get; set; }
        public string Comment { get; set; }
        public bool IsActive { get; set; }

        private int index = 0;
        public DateTime DateCreate { get; set; }
        public DateTime DateExecution { get; set; }


        public ApplicationAction(int appid, int employeeid)
        {
            Id = index++;
            AppID = appid;
            EmployeeID = employeeid;
            ApplicationState = Program.RepositoryApplicationState.FindItem(1);
            IsActive = false;

        
        }

        



    }
}
