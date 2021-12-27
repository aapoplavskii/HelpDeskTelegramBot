using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class GetEmployees
    {
        public static List<Employee> GetEmpolyee()
        {
            using (var db  = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                var table = db.GetTable<Employee>();
                var list = table.ToList();
                return list;
            }
        
        
        
        }

    }
}
