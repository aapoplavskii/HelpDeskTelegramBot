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
        
        public List<ApplicationState> applicationstates = new List<ApplicationState>();

        public List<Employee> employees = new List<Employee>();

        public List<TypeApplication> typeApplications = new List<TypeApplication>();

        public List<PositionEmployee> positions = new List<PositionEmployee>();

        public List<Department> departments = new List<Department>();

        public List<Building> buildings = new List<Building>();

        public Repository()
        { 
                        
            typeApplications.Add(new TypeApplication(1, "Ремонт ПК/принтера"));
            typeApplications.Add(new TypeApplication(2, "Замена картриджа"));
            typeApplications.Add(new TypeApplication(3, "Проблемы с МИС"));
            typeApplications.Add(new TypeApplication(4, "Проблемы с сетью"));
            typeApplications.Add(new TypeApplication(5, "Прочее"));

            positions.Add(new PositionEmployee(1, "Директор"));
            positions.Add(new PositionEmployee(2, "Врач"));
            positions.Add(new PositionEmployee(3, "Сотрудник администрации"));
            positions.Add(new PositionEmployee(4, "Научный сотрудник"));
            positions.Add(new PositionEmployee(5, "Медицинский инженер"));

            departments.Add(new Department(1, "Администрация"));
            departments.Add(new Department(2, "Поликлиника"));
            departments.Add(new Department(3, "Клинические подразделения"));
            departments.Add(new Department(4, "Научные подразделения"));
            departments.Add(new Department(5, "Технический департамент"));

            buildings.Add(new Building(1, "2.1"));
            buildings.Add(new Building(2, "2.2"));
            buildings.Add(new Building(3, "3"));
            buildings.Add(new Building(4, "5"));
            buildings.Add(new Building(5, "7"));

            applicationstates.Add(new ApplicationState(1, "подана"));
            applicationstates.Add(new ApplicationState(2, "в работе"));
            applicationstates.Add(new ApplicationState(3, "исполнена"));

        }
        //подумать - сделать дженерик метод
        public Employee FindFIOSotr(string sotr)
        {
            return employees.FirstOrDefault(s => s.FIO.Contains(sotr));
        }

        public TypeApplication FindTypeApplication(int id)
        {
            return typeApplications.FirstOrDefault(s => s.Id == id);
        }

        public PositionEmployee FindPositionEmployee(int id)
        {
            return positions.FirstOrDefault(s => s.Id == id);
        }

        public Department FindDepartament(int id)
        {
            return departments.FirstOrDefault(s => s.Id == id);
        }

        public Building FindBuilding(int id)
        {
            return buildings.FirstOrDefault(s => s.Id == id);
        }

        public ApplicationState FindStateApplication(int id)
        {
            return applicationstates.FirstOrDefault(s => s.Id == id);
        }

    }
}
