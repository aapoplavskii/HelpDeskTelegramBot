using System.Collections.Generic;
using System.Linq;

namespace TelegramBot
{
    public static class GetApplicationsSQL
    {
        public static List<string> FindAll(long chatID, IApplicationRepository repositoryApplications,
            IRepositoryAdditionalDatabases<TypeApplication> repositoryTypeApplication, IRepositoryEmployees repositoryEmployees,
            IRepositoryAdditionalDatabases<ApplicationState> repositoryApplicationState)
        {
            List<string> messageapp = new List<string>();
            List<int> listID = new List<int>();


            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                var query = from application in db.GetTable<Application>()
                            join employee in db.GetTable<Employee>()
                            on application.EmployeeID equals employee.ID
                            where employee.Chat_ID == chatID
                            select application.ID;
                               
                listID = query.ToList();
                                               

            

            foreach (var item in listID)
            {
                var ouraction = db.GetTable<ApplicationAction>().Where(s => s.AppID == item).
                                                                                  OrderByDescending(i => i.ApplicationStateID).
                                                                                  Select(s => s).
                                                                                  Take(1).
                                                                                  FirstOrDefault();

                if (ouraction != null)
                {

                        var app = repositoryApplications.FindItem(ouraction.AppID);
                        

                        var message = "Заявка № - " + app.ToString()+ ", тип - " 
                                      + repositoryTypeApplication.FindItem(app.TypeApplicationID)
                                      + ", текст - " + app.Content
                                      + ", состояние - " + repositoryApplicationState.FindItem(ouraction.ApplicationStateID);

                    switch (ouraction.ApplicationStateID)
                    {
                        case 1:
                                messageapp.Add(message + ", исполнитель не назначен");
                            break;
                        case 2:
                                messageapp.Add(message + ", исполнитель - " + repositoryEmployees.FindItem(ouraction.EmployeeID).FIO);
                            break;

                    }

                }
            }

            }

            return messageapp;


        }
        
    }
}

