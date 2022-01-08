namespace TelegramBot
{
    public class Building: BaseEntity
    {
        public string Name { get; set; }

        public Building(int id, string name)
        { 
            this.Id = id;   
            this.Name = name;
        
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
