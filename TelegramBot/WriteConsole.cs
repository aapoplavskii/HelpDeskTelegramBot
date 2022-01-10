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

                    var exit = WriteMenu(repository, employee);

                    if (exit)
                        break;
                }

               
            }


        }

        private static bool WriteMenu(Repository repository, Employee employee)
        {
            bool exit = false;

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

                        var newapplication = SubmitNewApplication(repository, employee);
                        Console.WriteLine($"Заявка под номером - {newapplication.Id} успешно создана.");

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
                    exit = true;
                    break;
                }

            }
            return exit;


        }

        private static void ViewApplication(Repository repository)
        {
            throw new NotImplementedException();
        }

        private static Application SubmitNewApplication(Repository repository, Employee employee)
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
                        typeApplication = repository.FindItem(1, repository.typeApplications);
                        break;
                    case '2':
                        typeApplication = repository.FindItem(2, repository.typeApplications);
                        break;
                    case '3':
                        typeApplication = repository.FindItem(3, repository.typeApplications);
                        break;
                    case '4':
                        typeApplication = repository.FindItem(4, repository.typeApplications);
                        break;
                    case '5':
                        typeApplication = repository.FindItem(5, repository.typeApplications);
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
            

            Building building = null;
            while (true)
            {

                Console.WriteLine("Выберите корпус");
                Console.WriteLine("1. 2.1");
                Console.WriteLine("2. 2.2");
                Console.WriteLine("3. 3");
                Console.WriteLine("4. 5");
                Console.WriteLine("5. 7");


                var otvet = Console.ReadKey();
                Console.WriteLine();

                switch (otvet.KeyChar)
                {
                    case '1':
                        building = repository.FindItem(1, repository.buildings);
                        break;
                    case '2':
                        building = repository.FindItem(2, repository.buildings);
                        break;
                    case '3':
                        building = repository.FindItem(3, repository.buildings);
                        break;
                    case '4':
                        building = repository.FindItem(4, repository.buildings);
                        break;
                    case '5':
                        building = repository.FindItem(5, repository.buildings);
                        break;
                    default:
                        Console.WriteLine("Необходимо выбрать из списка!");
                        break;

                }

                if (building != null)
                {
                    Console.WriteLine();
                    break;
                }

            }

            Console.WriteLine("");
            Console.Write("Введите номер кабинета:");
            var room  = Console.ReadLine();
            
            Console.WriteLine("");
            Console.Write("Введите конт. тел.:");
            var phone = Console.ReadLine();

            Console.WriteLine("");
            Console.Write("Введите описание заявки:");
            var content = Console.ReadLine();

            return new Application(typeApplication, employee, building, room, phone, content, repository.FindItem(1, repository.applicationstates), false);

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
                        positionEmployee = repository.FindItem(1, repository.positions);
                        break;
                    case '2':
                        positionEmployee = repository.FindItem(2, repository.positions);
                        break;
                    case '3':
                        positionEmployee = repository.FindItem(3, repository.positions);
                        break;
                    case '4':
                        positionEmployee = repository.FindItem(4, repository.positions);
                        break;
                    case '5':
                        positionEmployee = repository.FindItem(5, repository.positions);
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
                            department = repository.FindItem(1, repository.departments);
                            break;
                        case '2':
                            department = repository.FindItem(2, repository.departments);
                            break;
                        case '3':
                            department = repository.FindItem(3, repository.departments);
                            break;
                        case '4':
                            department = repository.FindItem(4, repository.departments);
                            break;
                        case '5':
                            department = repository.FindItem(5, repository.departments);
                            break;
                        default:
                            Console.WriteLine("Необходимо выбрать из списка!");
                            break;

                    }

                if (department != null)
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

