using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public static class WorkWithApplication
    {
        public static void SubmitApplication(Application application, Repository repository)
        { 
                
            repository.applications.Add(application);
        
        }

    }
}
