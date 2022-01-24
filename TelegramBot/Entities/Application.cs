using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    [Table(Name = "Application")]
    public class Application : BaseEntity
    {
        public TypeApplication TypeApplication { get; set; }
        
        [Column(Name = "TypeApplicationID")]
        public int TypeApplicationID { get; set; }
        public Employee Employee { get; set; }

        [Column(Name = "EmployeeID")]
        public int EmployeeID { get; set; }
        public Building Building { get; set; }

        [Column(Name = "BuildingID")]
        public int BuildingID { get; set; }

        [Column(Name = "Room")]
        public string Room { get; set; }

        [Column(Name = "ContactTelephone")]
        public string ContactTelephone { get; set; }

        [Column(Name = "Content")]
        public string Content { get; set; }

        [Column(Name = "IsDelete")]
        public bool IsDelete { get; set; }

        private int index = 2;

        [Column(Name = "statewrite")]
        public int statewrite { get; set; }
        public Application(Employee employee)
        {
            ID = index++;
            Employee = employee;
            EmployeeID = employee.ID;
            statewrite = 0;
            IsDelete = true;
        }

        public Application() {}

        public override string ToString()
        {
            return ID.ToString();
        }


    }
}
