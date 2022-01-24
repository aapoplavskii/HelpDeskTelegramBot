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
                

        public Application AddNewApp(long chatID)
        {
            var employee = Program.RepositoryEmployees.FindItemChatID(chatID);

            var newapp = new Application(employee);
            _applications.Add(newapp);

            return newapp;
        
        }

        public void ChangeState(int appID, int state)
        {
            var application = FindItem(appID);

            application.statewrite = state;
        }
        public void UpdateTypeApp(int appID, TypeApplication typeApplication)
        {
            var app = FindItem(appID);

            app.TypeApplication = typeApplication;
            app.TypeApplicationID = typeApplication.ID;
            
        }
        public void UpdateBuildingApp(int appID, Building building)
        {
            var app = FindItem(appID);

            app.Building = building;
            app.BuildingID = building.ID;

        }

        public void UpdateRoomApp(int appID, string room)
        {
            var app = FindItem(appID);

            app.Room = room;

        }
        public void UpdatePhoneApp(int appID, string phone)
        {

            var app = FindItem(appID);
            app.ContactTelephone = phone;
        }

        public void UpdateContentApp(int appID, string content)
        {
            var app = FindItem(appID);
            app.Content = content;
        }
        public List<Application> GetListApp() => _applications;
        
    }
}
