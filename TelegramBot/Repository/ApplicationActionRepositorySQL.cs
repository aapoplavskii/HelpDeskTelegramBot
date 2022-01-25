using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TelegramBot
{
    public class ApplicationActionRepositorySQL : IApplicationActionRepository
    {
        public ApplicationAction AddNewAppAction(int appID, int employeeID)
        {
           
            var newapp = new ApplicationAction(appID, employeeID);

            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                var table = db.Insert(newapp);
            }

            return newapp;
        }

        public void ChangeState(ApplicationAction applicationAction, ApplicationState state)
        {
            applicationAction.ApplicationStateID = state.ID;

            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                var table = db.Update(applicationAction);
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

            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                var table = db.Update(applicationAction);
            }
        }
    }
}
