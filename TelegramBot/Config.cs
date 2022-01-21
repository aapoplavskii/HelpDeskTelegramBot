using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public static class Config
    {
        public static string SqlConnectionString = "User ID=postgres; Password = 'Kontroller-1394';Host=localhost;Port=5432;Database=TelegramBot";

        public static DataConnection db = new DataConnection(LinqToDB.ProviderName.PostgreSQL, SqlConnectionString);
    }
}
