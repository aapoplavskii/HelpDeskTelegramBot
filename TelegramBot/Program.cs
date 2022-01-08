using System;

namespace TelegramBot
{
    internal class Program
    {
        public static Repository repository = new Repository();
        static void Main(string[] args)
        {
            //Bot.InitBot();

            WriteConsole.WriteData(repository);
            Console.ReadLine();

        }


    }
}
