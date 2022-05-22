using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public class ApplicationActionRepositorySQL : IApplicationActionRepository
    {
        public void AddNewAppAction(int appID, int employeeID, int stateID)
        {
            

            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                var table = db.GetTable<ApplicationAction>();
                table.Value(p => p.AppID, appID)
                     .Value(p => p.EmployeeID, employeeID)  
                     .Value(p => p.ApplicationStateID, stateID)
                     .Value(p => p.DateWriteRecord, DateTime.Now)
                     .Insert();
                

            }

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

        public List<List<InlineKeyboardButton>> GetInlineKeyboardButtons()
        {
            var result = new List<List<InlineKeyboardButton>>();

            var buttons = new List<ApplicationAction>();

            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                buttons = db.GetTable<ApplicationAction>().Select(x => x).ToList();
            }

            var rowcount = (int)Math.Ceiling(buttons.Count() / 2f);

            for (int i = 0; i < rowcount; i++)
            {
                var list = new List<InlineKeyboardButton>();

                list.AddRange(buttons.Skip(i * 2).Take(2).Select(i => InlineKeyboardButton.WithCallbackData(i.Name, i.Name)));

                result.Add(list);
            }

            return result;

        }
    }
}
