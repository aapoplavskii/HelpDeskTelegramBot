using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public interface IRepository
    {       
        public List<Application> applications { get; set; }

        public List<ApplicationState> applicationstates { get; set; }

        public List<Employee> employees { get; set; }

        public List<TypeApplication> typeApplications { get; set; }

        public List<PositionEmployee> positions { get; set; }

        public List<Department> departments { get; set; }

        public List<Building> buildings { get; set; }

        public T FindItem<T>(int id, List<T> list) where T : BaseEntity;

        public Employee FindFIOSotr(string sotr);
    
    }
}
