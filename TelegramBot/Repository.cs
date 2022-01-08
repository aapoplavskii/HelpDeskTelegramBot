using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class Repository
    {
        public List<Application> applications  = new List<Application>();

        public List<Employee> employees = new List<Employee>();

        public List<TypeApplication> typeApplications = new List<TypeApplication>();

        public List<PositionEmployee> positions = new List<PositionEmployee>();

        public List<Department> departments = new List<Department>();

        public List<Building> buildings = new List<Building>();

        //создать дженерик методы по добавлению и поиску по листам

        //или вообще создать дженерик класс

        public Repository()
        { 
            employees.Add(new Employee(1, "Поплавский"));
        
        }

        public Employee FindFIOSotr(string sotr)
        {
            return employees.FirstOrDefault(s => s.FIO.Contains(sotr));
        }

    }
}
