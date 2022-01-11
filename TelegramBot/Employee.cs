using LinqToDB.Mapping;

namespace TelegramBot
{
    public class Employee: BaseEntity
    {
        public string FIO { get; set; }

        public PositionEmployee Position { get; set; }

        public Department Department { get; set; }

        public string Chat_ID   { get; set; }

        public string Phone_number { get; set; }

        public Employee(int id, string fio, PositionEmployee position, Department department)
        {

            this.Id = id;
            this.FIO = fio;
            this.Position = position;
            this.Department = department;
                    
        }

        public override string ToString()
        {
            return FIO + "(" + Position + "-" + Department + ")";
        }



    }
}
