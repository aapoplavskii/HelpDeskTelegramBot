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


        public void UpdateTypeApp(Application app, TypeApplication typeApplication, int statewrite)
        {
            app.TypeApplication = typeApplication; 
            app.statewrite = statewrite;

        }
        public void UpdateBuildingApp(Application app, Building building, int statewrite)
        {
            app.Building = building;
            app.statewrite = statewrite;

        }

        public void UpdateRoomApp(Application app, string room, int statewrite)
        {
            app.Room = room;
            app.statewrite = statewrite;

        }

        public void UpdatePhoneApp(Application app, string phone, int statewrite)
        {
            app.ContactTelephone = phone;
            app.statewrite = statewrite;

        }

        public void UpdateContentApp(Application app, string content, int statewrite)
        {
            app.Content = content;
            app.statewrite = statewrite;

        }
    }
}
