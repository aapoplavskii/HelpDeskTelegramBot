using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public static class Command
    {

        public static Employee InitUser(string user)
        {

            return Program.repository.employees.FirstOrDefault(s => s.FIO.Contains(user));

            //using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            //{
            //    var table = db.GetTable<Employee>();
            //    var list = table.ToList();
            //    return list;
            //}
        }

        public static Employee SubmitNewUser()
        {
            throw new NotImplementedException();
        }

        public static Application SubmitNewApplication()
        {
            throw new NotImplementedException();
        }
    }
}
