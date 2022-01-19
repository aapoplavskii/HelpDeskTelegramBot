﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class ApplicationActionRepository
    {
        public List<ApplicationAction> ApplicationsAction { get; set; } = new List<ApplicationAction>();

        public void AddNewAppAction(int appID, int employeeID)
        {
            var newappaction = new ApplicationAction(appID, employeeID);
            ApplicationsAction.Add(newappaction);
        
        }
        public void ChangeState(ApplicationAction applicationAction, ApplicationState state)
        {
            applicationAction.ApplicationState = state;
        }
     
        public void SetDate(ApplicationAction applicationAction, DateTime dateTime)
        {
            applicationAction.DateWriteRecord = dateTime;

        }
                

    }
}
