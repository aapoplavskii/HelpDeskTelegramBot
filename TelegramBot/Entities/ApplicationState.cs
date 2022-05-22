using LinqToDB.Mapping;

namespace TelegramBot
{
    [Table(Name = "ApplicationState")]
    public class ApplicationState : BaseEntity
    {
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
