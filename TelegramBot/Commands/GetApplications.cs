using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public static class GetApplications
    {
        public static List<Application> FindAll(long chatid)
        {

            var listapp = from application in Program.RepositoryApplications.Applications
                                join employee in Program.RepositoryEmployees.Employees on application.EmployeeID equals employee.Id
                                join applicationaction in Program.RepositoryApplicationActions.ApplicationsAction on application.Id equals applicationaction.AppID
                                where employee.Id == chatid && applicationaction.AppState == 1
                                select application;
            
            return listapp.ToList();

        }

    }
}
