using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class ApplicationRepositorySQL : IApplicationRepository
    {
        public Application AddNewApp(long chatID)
        {
            var employee = Program.RepositoryEmployees.FindItemChatID(chatID);

            var newapp = new Application(employee);

            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                var table = db.Insert(newapp);
            }
            return newapp;
        }

        public void ChangeState(int appID, int state)
        {
            var application = FindItem(appID);

            application.statewrite = state;

            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                var table = db.Update(application);
            }
        }

        public Application FindItem(int id)
        {
            Application item = null;
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                item = db.GetTable<Application>().FirstOrDefault(x => x.ID == id);
            }

            return item;
        }

        public List<Application> GetListApp()
        {
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {

                var table = db.GetTable<Application>();
                var list = table.ToList();
                return list;


            }
        }

        public void UpdateBuildingApp(int appID, Building building)
        {
            var app = FindItem(appID);

            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                app.BuildingID = building.ID;

                var table = db.Update(app);

            }
        }

        public void UpdateContentApp(int appID, string content)
        {
            var app = FindItem(appID);

            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                app.Content = content;

                var table = db.Update(app);

            }
        }

        public void UpdatePhoneApp(int appID, string phone)
        {
            var app = FindItem(appID);

            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                app.ContactTelephone = phone;

                var table = db.Update(app);

            }
        }

        public void UpdateRoomApp(int appID, string room)
        {
            var app = FindItem(appID);

            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                app.Room = room;

                var table = db.Update(app);

            }
        }

        public void UpdateTypeApp(int appID, TypeApplication typeApplication)
        {
            var app = FindItem(appID);

            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                app.TypeApplicationID = typeApplication.ID;

                var table = db.Update(app);

            }
        }
    }
}
