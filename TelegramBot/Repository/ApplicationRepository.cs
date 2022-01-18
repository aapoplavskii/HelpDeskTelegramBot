﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class ApplicationRepository : IApplicationRepository
    {
        public List<Application> Applications { get; set; } = new List<Application>();

        public Application FindItem(int id)
        {
            return Applications.FirstOrDefault(s => s.Id == id);
        }
                

        public Application AddNewApp(Employee employee)
        {
            var newapp = new Application(employee);
            Applications.Add(newapp);

            return newapp;
        
        }

        public void ChangeState(Application application, int state)
        {
            application.statewrite = state;
        }
        public void UpdateTypeApp(Application app, TypeApplication typeApplication, int state)
        {
            app.TypeApplication = typeApplication;
            app.statewrite = state;    
            
        }
        public void UpdateBuildingApp(Application app, Building building, int state)
        {
            app.Building = building;
            app.statewrite = state;

        }

        public void UpdateRoomApp(Application app, string room) => app.Room = room;
       
        public void UpdatePhoneApp(Application app, string phone) => app.ContactTelephone = phone;
        

        public void UpdateContentApp(Application app, string content) => app.Content = content;
        
    }
}
