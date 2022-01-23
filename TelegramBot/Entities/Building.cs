using LinqToDB.Mapping;

namespace TelegramBot
{
    [Table (Name = "Building")]
    public class Building: BaseEntity
    {
        [Column (Name = "Name")]
        public string Name { get; set; }

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
