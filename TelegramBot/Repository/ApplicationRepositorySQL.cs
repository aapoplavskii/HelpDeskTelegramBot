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
        public Application AddNewApp(Employee employee)
        {
            var newapp = new Application(employee);

            using (Config.db)
            {
                var table = Config.db.Insert(newapp);
            }
            return newapp;
        }

        public void ChangeState(Application application, int state)
        {
            application.statewrite = state;

            using (Config.db)
            {
                var table = Config.db.Update(application.statewrite);
            }
        }

        public Application FindItem(int id)
        {
            Application item = null;
            using (Config.db)
            {
                item = Config.db.GetTable<Application>().FirstOrDefault(x => x.ID == id);
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

        public void UpdateBuildingApp(Application app, Building building, int state)
        {
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                app.BuildingID = building.ID;

                var table = db.Update(app);

            }
        }

        public void UpdateContentApp(Application app, string content)
        {
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                app.Content = content;

                var table = db.Update(app);

            }
        }

        public void UpdatePhoneApp(Application app, string phone)
        {
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                app.ContactTelephone = phone;

                var table = db.Update(app);

            }
        }

        public void UpdateRoomApp(Application app, string room)
        {
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                app.Room = room;

                var table = db.Update(app);

            }
        }

        public void UpdateTypeApp(Application app, TypeApplication typeApplication, int state)
        {
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                app.TypeApplicationID = typeApplication.ID;
                app.statewrite = state;

                var table = db.Update(app);

            }
        }
    }
}
