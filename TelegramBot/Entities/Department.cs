using LinqToDB.Mapping;

namespace TelegramBot
{
    [Table(Name = "Department")]
    public class Department : BaseEntity
    {
        [Column(Name = "Name")]
        public string Name { get; set; }

        public Department(int id, string name)
        {
            this.Name = name;
            this.ID = id;

        }

        public Department() { }

        public override string ToString()
        {
            return Name;
        }

    }
}
