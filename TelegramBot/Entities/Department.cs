using LinqToDB.Mapping;

namespace TelegramBot
{
    [Table(Name = "Department")]
    public class Department : BaseEntity
    {
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
