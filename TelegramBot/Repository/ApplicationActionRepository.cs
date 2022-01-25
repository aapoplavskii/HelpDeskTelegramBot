using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class ApplicationActionRepository: IApplicationActionRepository
    {
        private List<ApplicationAction> _applicationsAction { get; set; } = new List<ApplicationAction>();

        public ApplicationAction AddNewAppAction(int appID, int employeeID)
        {
            var newappaction = new ApplicationAction(appID, employeeID);
            _applicationsAction.Add(newappaction);

            return newappaction;
        
        }
        public void ChangeState(ApplicationAction applicationAction, ApplicationState state)
        {
            applicationAction.ApplicationState = state;

        }
     
        public void SetDate(ApplicationAction applicationAction, DateTime dateTime)
        {
            applicationAction.DateWriteRecord = dateTime;

        }

        public List<ApplicationAction> GetListApp() => _applicationsAction;

    }
}
