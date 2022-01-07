using LinqToDB.Mapping;

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

        public Position Position { get; set; }

        public Department Department { get; set; }

        public Building Building { get; set; }

        public string Room { get; set; }

        [Column(Name = "ContactTelephone")]
        public string ContactTelephone { get; set; }

        [Column(Name = "Content")]
        public string Content { get; set; }


    }
}
