using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class ApplicationActionRepository
    {
        public List<ApplicationAction> ApplicationsAction { get; set; } = new List<ApplicationAction>();

        public ApplicationAction AddNewAppAction(int appID, int employeeID)
        {
            var newappaction = new ApplicationAction(appID, employeeID);
            ApplicationsAction.Add(newappaction);

            return newappaction;

        }

        public void ChangeState(ApplicationAction applicationAction, ApplicationState state)
        {
            applicationAction.ApplicationState = state;
        }

        public void ChangeActiveКecord(ApplicationAction applicationAction, bool isActive)
        {
            applicationAction.IsActive = isActive;
        }

        public void SetDate(ApplicationAction applicationAction, DateTime dateTime)
        {
            applicationAction.DateCreate = dateTime;

        }

        public void SetDateExecution(ApplicationAction applicationAction, DateTime dateTime)
        {
            applicationAction.DateExecution = dateTime;

        }

    }
}
