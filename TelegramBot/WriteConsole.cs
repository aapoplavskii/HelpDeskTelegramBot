using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public static class WriteConsole
    {
        
        public static void WriteData(Repository repository)
        {
            while (true)
            {
                Console.Write("Введите ФИО пользователя:");
                var FIOsotr = Console.ReadLine();
                
                var sotr = repository.FindFIOSotr(FIOsotr);

                if (sotr == null)
                {
                    Console.WriteLine("Сотрудник с такой Фамилией не найден. Создать нового пользователя? (y/n)");

                    while (true)
                    { 
                        var otvet = Console.ReadKey();

                        switch (otvet.Key)
                        { 
                            case ConsoleKey.Y:
                                Console.WriteLine();
                                var newemployee = SubmitNewEmployee();
                                break;
                            case ConsoleKey.N:
                                break;
                                                           
                        }

                        Console.WriteLine("Не понял Вашего ответа. Вводите только символы y/n.");
                    }
                
                }

                var exit = Console.ReadKey();
                if (exit.Key == ConsoleKey.Backspace)
                {
                    break;
                }
            }
        

        }

        private static Employee SubmitNewEmployee()
        {
            Console.WriteLine("Введите ID пользователя");
            if(!int.TryParse(Console.ReadLine(), out int idnewsotr))
                Console.WriteLine("Вводите только цифры!"); ; 
            Console.WriteLine("Введите ФИО пользователя");
            var fionewsotr = Console.ReadLine();

            var newemployee = new Employee(idnewsotr, fionewsotr);

            return newemployee;

        }
    }
}
