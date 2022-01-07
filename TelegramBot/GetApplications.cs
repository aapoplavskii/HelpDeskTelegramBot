using System.Collections.Generic;
using System.Linq;

namespace TelegramBot
{
    public class GetApplications
    {
        public static List<Application> GetApplication()
        {
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                var table = db.GetTable<Application>();
                var list = table.ToList();
                return list;
            }



        }

    }
}
