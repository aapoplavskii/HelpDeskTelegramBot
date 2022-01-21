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
            return _applications.FirstOrDefault(s => s.ID == id);
        }
                

        public Application AddNewApp(Employee employee)
        {
            var newapp = new Application(employee);
            _applications.Add(newapp);

            return newapp;
        
        }

        public void ChangeState(Application application, int state)
        {
            application.statewrite = state;
        }
        public void UpdateTypeApp(Application app, TypeApplication typeApplication, int state)
        {
            app.TypeApplication = typeApplication;
            app.TypeApplicationID = typeApplication.ID;
            app.statewrite = state;    
            
        }
        public void UpdateBuildingApp(Application app, Building building, int state)
        {
            app.Building = building;
            app.BuildingID = building.ID;
            app.statewrite = state;

        }

        public void UpdateRoomApp(Application app, string room) => app.Room = room;
       
        public void UpdatePhoneApp(Application app, string phone) => app.ContactTelephone = phone;
        

        public void UpdateContentApp(Application app, string content) => app.Content = content;

        public List<Application> GetListApp() => _applications;
        
    }
}
