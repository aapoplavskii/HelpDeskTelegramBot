using LinqToDB.Mapping;

namespace TelegramBot
{
    [Table(Name = "Building")]
    public class Building : BaseEntity
    {
        public Building(int id, string name)
        {
            ID = id;
            Name = name;

        }

        public Building() { }
        public override string ToString()
        {
            return Name;
        }

    }
}
