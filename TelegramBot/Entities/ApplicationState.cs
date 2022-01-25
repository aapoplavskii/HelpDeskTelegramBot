using LinqToDB.Mapping;

namespace TelegramBot
{
    [Table(Name = "ApplicationState")]
    public class ApplicationState : BaseEntity
    {
        [Column(Name = "Name")]
        public string Name { get; set; }

        public ApplicationState(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
        public ApplicationState() { }

        public override string ToString()
        {
            return Name;
        }

    }
}
