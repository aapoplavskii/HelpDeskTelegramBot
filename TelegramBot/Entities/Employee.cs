using LinqToDB.Mapping;

namespace TelegramBot
{
    public class Employee: BaseEntity
    {
        public string FIO { get; set; }

        public PositionEmployee Position { get; set; }

        public Department Department { get; set; }

        public long Chat_ID   { get; set; }
        
        public int State { get; set; }

        private int index = 1;

        public bool IsExecutor { get; set; }

        public Employee(long chatID)
        { 
           Chat_ID = chatID;
           Id = index++;
           State = 0;
        
        }

        

        public override string ToString()
        {
            return FIO + "(" + Position + "-" + Department + ")";
        }



    }
}
