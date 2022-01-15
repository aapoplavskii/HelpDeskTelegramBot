using System;

namespace TelegramBot
{
    public class Program
    {
        
        public static RepositoryEmployees RepositoryEmployees = new RepositoryEmployees();
        public static RepositoryPositions RepositoryPositions = new RepositoryPositions(); 
        static void Main(string[] args)
        {
            var bot = new Bot();
            bot.InitBot();

            //WriteConsole.WriteData(repository);
            

        }


    }
}
