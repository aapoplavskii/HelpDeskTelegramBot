using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    [Table(Name = "Employee")]
    public class Employee
    {
        [PrimaryKey]
        [Column(Name = "ID")]
        public int Id { get; set; }

        [Column(Name = "FIO")]
        public string Surname { get; set; }

        [Column(Name = "ContactTelephone")]
        public string ContactTelephone { get; set; }

        [Column(Name = "Content")]
        public string Content { get; set; }

       
    }
}
