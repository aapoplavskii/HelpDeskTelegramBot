using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramBot
{
    public static class Command
    {
        private static string ReturnTextMessageForTechEmployee(int appID, int employeeID)
        {
            var ouremployee = Program.RepositoryEmployees.FindItem(employeeID);

            var ourapp = Program.RepositoryApplications.FindItem(appID);

            var mes = $"Новая заявка id = {appID} от {ouremployee} ( {Program.RepositoryDepartment.FindItem(ouremployee.DepartmentID)})" +
                    $"\n({Program.RepositoryBuildings.FindItem(ourapp.BuildingID)}, {ourapp.Room}, {ourapp.ContactTelephone})" +
                    $"\nтекст - {Program.RepositoryApplications.FindItem(appID).Content}";

            return mes;
        
        }

        public static async Task SendMessageForTechEmployee(int appID, int employeeID, ITelegramBotClient botClient, CancellationToken cancellationToken)
        {
            var textfortechemployee = ReturnTextMessageForTechEmployee(appID, employeeID);

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

        

    }
}
