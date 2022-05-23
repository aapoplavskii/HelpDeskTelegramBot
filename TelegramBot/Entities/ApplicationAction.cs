using LinqToDB.Mapping;
using System;

namespace TelegramBot
{
    [Table(Name = "ApplicationAction")]
    public class ApplicationAction 
    {
        [PrimaryKey]
        [Column(Name = "ID")]
        public int ID { get; set; }

        [Column(Name = "ApplicationID")]
        public int AppID { get; set; }

        [Column(Name = "EmployeeID")]
        public int EmployeeID { get; set; }
        public ApplicationState ApplicationState { get; set; }

        [Column(Name = "ApplicationStateID")]
        public int ApplicationStateID { get; set; }

        [Column(Name = "Comment")]
        public string Comment { get; set; }

        [Column(Name = "DateWriteRecord")]
        public DateTime DateWriteRecord { get; set; }

        public ApplicationAction() { }

    }
}
