using LinqToDB.Mapping;

namespace TelegramBot
{
    [Table(Name = "Employee")]
    public class Employee
    {
        [PrimaryKey]
        [Column(Name = "ID")]
        public int ID { get; set; }

        [Column(Name = "FIO")]
        public string FIO { get; set; }

        public PositionEmployee Position { get; set; }

        [Column(Name = "PositionEmployeeID")]
        public int PositionEmployeeID { get; set; }

        public Department Department { get; set; }

        [Column(Name = "DepartmentID")]
        public int DepartmentID { get; set; }

        [Column(Name = "ChatID")]
        public long Chat_ID { get; set; }

        [Column(Name = "State")]
        public int State { get; set; }

        [Column(Name = "isExecutor")]
        public bool IsExecutor { get; set; }

        public Employee(long chatID)
        {
           Chat_ID = chatID;
           State = 0;

        }

        public Employee() { }

        public override string ToString()
        {
            return FIO;
        }



    }
}
