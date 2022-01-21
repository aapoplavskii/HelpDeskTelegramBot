using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class ApplicationActionRepositorySQL : IApplicationActionRepository
    {
        public void AddNewAppAction(int appID, int employeeID)
        {
            var ouremployee = Program.RepositoryEmployees.FindItem(employeeID);
            var newapp = new ApplicationAction(appID,employeeID);
            
            using (Config.db)
            {
                var table = Config.db.Insert(newapp);
            }
        }

        public void ChangeState(ApplicationAction applicationAction, ApplicationState state)
        {
            applicationAction.ApplicationStateID = state.ID;
            
            using (Config.db)
            {
                var table = Config.db.Update(applicationAction);
            }
        }

        public List<ApplicationAction> GetListApp()
        {
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {

                var table = db.GetTable<ApplicationAction>();
                var list = table.ToList();
                return list;


            }
        }

        public void SetDate(ApplicationAction applicationAction, DateTime dateTime)
        {
            applicationAction.DateWriteRecord = dateTime;

            using (Config.db)
            {
                var table = Config.db.Update(applicationAction);
            }
        }
    }
}
