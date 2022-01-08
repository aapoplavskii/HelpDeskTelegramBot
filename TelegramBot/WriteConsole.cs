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
            Employee employee;

            while (true)
            {
                Console.Write("Добрый день. Представьтесь, пожалуйста. Введите Вашу фамилию:");
                var FIOsotr = Console.ReadLine();

                employee = repository.FindFIOSotr(FIOsotr);

                if (employee == null)
                {
                    Console.WriteLine("Сотрудник с такой фамилией не найден. Создать нового пользователя? (Y/N)");

                    while (true)
                    {
                        var otvet = Console.ReadKey();

                        switch (otvet.Key)
                        {
                            case ConsoleKey.Y:
                                Console.WriteLine();
                                var newemployee = SubmitNewEmployee(repository);
                                employee = newemployee;
                                break;
                            case ConsoleKey.N:
                                Console.WriteLine();
                                break;
                            default:
                                Console.WriteLine();
                                Console.WriteLine("Не понял Вашего ответа. Вводите только символы (Y/N).");
                                Console.WriteLine();
                                break;
                        }

                        if (otvet.Key == ConsoleKey.Y || otvet.Key == ConsoleKey.N)
                            break;
                        
                        ;
                    }

                }

                if (employee != null)
                {
                    Console.WriteLine($"Вы работаете от имени - {employee}");

                    WriteMenu(repository);
                }

               
            }


        }

        private static void WriteMenu(Repository repository)
        {
            while (true)
            {
                Console.WriteLine("Для подачи новой заявки нажмите 1");
                Console.WriteLine("Для просмотра Ваших заявок в работе нажмите 2");
                Console.WriteLine("Для выхода из программы нажмите 3");

                var otvet = Console.ReadKey();
                Console.WriteLine();

                switch (otvet.KeyChar)
                {
                    case '1':

                        SubmitNewApplication(repository);

                        break;
                    case '2':
                        ViewApplication(repository);
                        break;
                    case '3':
                        break;
                    default:
                        Console.WriteLine("Необходимо ввести указанные символы!");
                        break;

                }

                if (otvet.KeyChar == '3')
                {
                    break;
                }

            }


        }

        private static void ViewApplication(Repository repository)
        {
            throw new NotImplementedException();
        }

        private static void SubmitNewApplication(Repository repository)
        {
            TypeApplication typeApplication = null;

            while (true)
            {

                Console.WriteLine("Выберите тип заявки");
                Console.WriteLine("1. Ремонт ПК/принтера");
                Console.WriteLine("2. Замена картриджа");
                Console.WriteLine("3. Проблемы с МИС");
                Console.WriteLine("4. Проблемы с сетью");
                Console.WriteLine("5. Прочее");


                var otvet = Console.ReadKey();
                Console.WriteLine();

                switch (otvet.KeyChar)
                {
                    case '1':
                        typeApplication = repository.FindTypeApplication(1);
                        break;
                    case '2':
                        typeApplication = repository.FindTypeApplication(2);
                        break;
                    case '3':
                        typeApplication = repository.FindTypeApplication(3);
                        break;
                    case '4':
                        typeApplication = repository.FindTypeApplication(4);
                        break;
                    case '5':
                        typeApplication = repository.FindTypeApplication(5);
                        break;
                    default:
                        Console.WriteLine("Необходимо выбрать из списка!");
                        break;

                }

                if (typeApplication != null)
                {
                    Console.WriteLine();
                    break;
                }

            }


        }

        private static Employee SubmitNewEmployee(Repository repository)
        {
            int idnewsotr = 0;

            while (true)
            {
                Console.WriteLine("Введите ID пользователя");

                if (int.TryParse(Console.ReadLine(), out idnewsotr))
                    break;

                Console.WriteLine("Вводите только цифры!");

            }

            Console.WriteLine("Введите ФИО пользователя");
            var fionewsotr = Console.ReadLine();

            PositionEmployee positionEmployee = null;

            while (true)
            {

                Console.WriteLine("Выберите должность");
                Console.WriteLine("1. Директор");
                Console.WriteLine("2. Врач");
                Console.WriteLine("3. Сотруник администрации");
                Console.WriteLine("4. Научный сотрудник");
                Console.WriteLine("5. Медицинский инженер");


                var otvetP = Console.ReadKey();
                Console.WriteLine();

                switch (otvetP.KeyChar)
                {
                    case '1':
                        positionEmployee = repository.FindPositionEmployee(1);
                        break;
                    case '2':
                        positionEmployee = repository.FindPositionEmployee(2);
                        break;
                    case '3':
                        positionEmployee = repository.FindPositionEmployee(3);
                        break;
                    case '4':
                        positionEmployee = repository.FindPositionEmployee(4);
                        break;
                    case '5':
                        positionEmployee = repository.FindPositionEmployee(5);
                        break;
                    default:
                        Console.WriteLine("Необходимо выбрать из списка!");
                        Console.WriteLine();
                        break;

                }
                if (positionEmployee != null)
                {
                    Console.WriteLine();
                    break;
                }
            }
                
            Department department = null;

                while (true)
                {

                    Console.WriteLine("Выберите подразделение");
                    Console.WriteLine("1. Администрация");
                    Console.WriteLine("2. Поликлиника");
                    Console.WriteLine("3. Клинические подразделения");
                    Console.WriteLine("4. Научные подразделения");
                    Console.WriteLine("5. Технический департамент");


                    var otvetD = Console.ReadKey();
                    Console.WriteLine();

                    switch (otvetD.KeyChar)
                    {
                        case '1':
                            department = repository.FindDepartament(1);
                            break;
                        case '2':
                            department = repository.FindDepartament(2);
                            break;
                        case '3':
                            department = repository.FindDepartament(3);
                            break;
                        case '4':
                            department = repository.FindDepartament(4);
                            break;
                        case '5':
                            department = repository.FindDepartament(5);
                            break;
                        default:
                            Console.WriteLine("Необходимо выбрать из списка!");
                            break;

                    }

                if (positionEmployee != null)
                {
                    Console.WriteLine();
                    break;
                }
            }


                var newemployee = new Employee(idnewsotr, fionewsotr, positionEmployee, department);

                return newemployee;

            }
        }
    }

