using LinqToDB.Mapping;

namespace TelegramBot
{
    public class Employee
    {
        public int Id { get; set; }

        public string FIO { get; set; }

        public PositionEmployee Position { get; set; }

        public Department Department { get; set; }

        public Employee(int id, string fio)
        {

            this.Id = id;
            this.FIO = fio;
        
        }



    }
}
