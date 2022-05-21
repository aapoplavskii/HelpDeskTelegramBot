using LinqToDB.Mapping;

namespace TelegramBot
{
    [Table(Name = "TypeApplication")]
    public class TypeApplication : BaseEntity
    {
        public TypeApplication(int id, string name)
        {
            this.Name = name;
            this.ID = id;

        }

        public TypeApplication() { }

        public override string ToString()
        {
            return Name;
        }


    }
}
