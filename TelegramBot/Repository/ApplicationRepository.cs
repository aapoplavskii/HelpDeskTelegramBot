using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class ApplicationRepository : IApplicationRepository
    {
        private List<Application> _applications { get; set; } = new List<Application>();

        public Application FindItem(int id)
        {
            return _applications.FirstOrDefault(s => s.Id == id);
        }

        public Application AddNewApp(Employee employee)
        {
            var newapp = new Application(employee);
            _applications.Add(newapp);

            return newapp;
        
        }
    }
}
