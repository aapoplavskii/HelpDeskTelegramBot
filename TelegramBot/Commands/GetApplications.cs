using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public static class GetApplications
    {
        public static List<ApplicationAction> FindAll(long chatid)
        {
            List<ApplicationAction> listapp = new List<ApplicationAction>();


            var listuserapp = (from application in Program.RepositoryApplications.GetListApp()
                              join employee in Program.RepositoryEmployees.GetListEmployee() on application.EmployeeID equals employee.ID
                              where employee.Chat_ID == chatid
                              select application.ID).ToList();
            
            foreach (var item in listuserapp)
            {
                var ouraction = Program.RepositoryApplicationActions.GetListApp().Where(s => s.AppID == item).
                                                                                  OrderByDescending(i => i.ApplicationStateID).
                                                                                  Select(s => s).
                                                                                  Take(1).
                                                                                  FirstOrDefault();

                if (ouraction != null)
                {
                    switch (ouraction.ApplicationStateID)
                    {
                        case 1:
                            listapp.Add(ouraction);
                            break;
                        case 2:
                            listapp.Add(ouraction);
                            break;                  
                    
                    } 
                    
                }
            }
            
            return listapp;

        }

    }
}
