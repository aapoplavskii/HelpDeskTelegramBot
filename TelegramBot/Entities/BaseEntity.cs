using LinqToDB.Mapping;

namespace TelegramBot
{
    public class BaseEntity
    {
        [PrimaryKey]
        [Column(Name = "ID")]
        public int ID { get; set; }

    }
}
