using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public static class Commands
    {
        public static string ReturnTextMessageForTechEmployee(ApplicationAction newapp)
        {
            var ouremployee = Program.RepositoryEmployees.FindItem(newapp.EmployeeID);

            var mes = $"Новая заявка id = {newapp.ID} от {ouremployee} ( {Program.RepositoryEmployees.FindItem(ouremployee.DepartmentID)})" +
                    $"\nтекст - {Program.RepositoryApplications.FindItem(newapp.AppID).Content}";

            return mes;
        
        }

    }
}
