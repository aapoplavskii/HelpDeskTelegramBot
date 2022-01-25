using LinqToDB.Mapping;

namespace TelegramBot
{
    [Table(Name = "PositionEmployee")]
    public class PositionEmployee : BaseEntity
    {
        [Column(Name = "Name")]
        public string Name { get; set; }

        public PositionEmployee(int id, string name)
        {
            this.ID = id;
            this.Name = name;

        }

        public PositionEmployee() { }

        public override string ToString()
        {
            return Name;
        }



    }
}
