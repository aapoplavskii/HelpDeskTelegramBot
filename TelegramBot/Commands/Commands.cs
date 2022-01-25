using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramBot
{
    public static class Commands
    {
        private static string ReturnTextMessageForTechEmployee(ApplicationAction newapp)
        {
            var ouremployee = Program.RepositoryEmployees.FindItem(newapp.EmployeeID);

            var ourapp = Program.RepositoryApplications.FindItem(newapp.AppID);

            var mes = $"Новая заявка id = {newapp.ID} от {ouremployee} ( {Program.RepositoryDepartment.FindItem(ouremployee.DepartmentID)})" +
                    $"\n({Program.RepositoryBuildings.FindItem(ourapp.BuildingID)}, {ourapp.Room}, {ourapp.ContactTelephone})" +
                    $"\nтекст - {Program.RepositoryApplications.FindItem(newapp.AppID).Content}";

            return mes;
        
        }

        public static async Task SendMessageForTechEmployee(ApplicationAction newappaction, ITelegramBotClient botClient, CancellationToken cancellationToken)
        {
            var textfortechemployee = ReturnTextMessageForTechEmployee(newappaction);

            var listtechemployee = Program.RepositoryEmployees.FindTechEmployee();

            foreach (var item in listtechemployee)
            {
                await botClient.SendTextMessageAsync(
                                chatId: item.Chat_ID,
                                text: textfortechemployee +
                                      "\nВзять в работу /take",
                                cancellationToken: cancellationToken);
            }
        }

        //public static async Task GetAppForTechEmployee()
        //{ 
        
        //}

    }
}
