using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Commands;

namespace TelegramBot
{
    public static class Command
    {
        private static string ReturnTextMessageForTechEmployee(int appID, int employeeID, IApplicationRepository repositoryApplications,
            IRepositoryAdditionalDatabases<Building> _repositoryBuildings, IRepositoryEmployees repositoryEmployees,
            IRepositoryAdditionalDatabases<Department> _repositoryDepartment)
        {
            var ouremployee = repositoryEmployees.FindItem(employeeID);

            var ourapp = repositoryApplications.FindItem(appID);

            var mes = $"Новая заявка id = {appID} от {ouremployee} ( {_repositoryDepartment.FindItem(ouremployee.DepartmentID)})" +
                    $"\n({_repositoryBuildings.FindItem(ourapp.BuildingID)}, {ourapp.Room}, {ourapp.ContactTelephone})" +
                    $"\nтекст - {repositoryApplications.FindItem(appID).Content}";

            return mes;

        }

        public static async Task SendMessageForTechEmployee(int appID, int employeeID, ITelegramBotClient botClient, CancellationToken cancellationToken,
            IApplicationRepository repositoryApplications,
            IRepositoryAdditionalDatabases<Building> _repositoryBuildings, IRepositoryEmployees repositoryEmployees,
            IRepositoryAdditionalDatabases<Department> _repositoryDepartment)
        {
            var textfortechemployee = ReturnTextMessageForTechEmployee(appID, employeeID, repositoryApplications, _repositoryBuildings, repositoryEmployees,
                _repositoryDepartment);

            var listtechemployee = repositoryEmployees.FindTechEmployee();

            foreach (var item in listtechemployee)
            {
                await botClient.SendTextMessageAsync(
                                chatId: item.Chat_ID,
                                text: textfortechemployee +
                                      "\nВзять в работу /take",
                                cancellationToken: cancellationToken);
            }
        }

        public static async void UpdatePositionUser(int id, IRepositoryAdditionalDatabases<PositionEmployee> repositoryPositions, IRepositoryEmployees repositoryEmployees,
            Update update, CancellationToken cancellationToken, ITelegramBotClient botClient, Employee ouremployee, 
            IRepositoryAdditionalDatabases<Department> repositoryDepartment, Dictionary<long, UserStates> clientStates, long chatId)
        {
            var position = repositoryPositions.FindItem(id);

            repositoryEmployees.UpdatePositionEmployee(update.CallbackQuery.Message.Chat.Id, position);

            var command = new RegNewUserCommand(repositoryEmployees, repositoryPositions, repositoryDepartment, clientStates,
                                        botClient, cancellationToken, update, ouremployee, chatId);

            await command.Execute(update);

        }

        public static async void UpdateDepartmentUser(int id, IRepositoryAdditionalDatabases<Department> repositoryDepartment, IRepositoryEmployees repositoryEmployees,
            Update update, CancellationToken cancellationToken, ITelegramBotClient botClient, Employee ouremployee,
            IRepositoryAdditionalDatabases<PositionEmployee> repositoryPositions, Dictionary<long, UserStates> clientStates, long chatId)
        {
            var department = repositoryDepartment.FindItem(id);

            repositoryEmployees.UpdateDepartmentEmployee(update.CallbackQuery.Message.Chat.Id, department);

            var command = new RegNewUserCommand(repositoryEmployees, repositoryPositions, repositoryDepartment, clientStates,
                                        botClient, cancellationToken, update, ouremployee, chatId);

            await command.Execute(update);

        }

        public static async void UpdateExecutorEmployee(IRepositoryEmployees repositoryEmployees, Update update, CancellationToken cancellationToken, ITelegramBotClient botClient,
            Employee ouremployee, bool tech, IRepositoryAdditionalDatabases<PositionEmployee> repositoryPositions, 
            Dictionary<long, UserStates> clientStates, IRepositoryAdditionalDatabases<Department> repositoryDepartment, long chatId)
        {
            repositoryEmployees.UpdateIsExecutorEmployee(update.CallbackQuery.Message.Chat.Id, tech);
            var command = new RegNewUserCommand(repositoryEmployees, repositoryPositions, repositoryDepartment, clientStates,
                                        botClient, cancellationToken, update, ouremployee, chatId);

            await command.Execute(update);
        }

        public static async void UpdateTypeApp(IRepositoryAdditionalDatabases<TypeApplication> repositoryTypeApplication, IApplicationRepository repositoryApplications, 
            Update update, ITelegramBotClient botClient, CancellationToken cancellationToken, Dictionary<long, UserStates> clientStates, Employee ouremployee,
            int id, IRepositoryEmployees repositoryEmployees, IApplicationActionRepository repositoryApplicationActions,
            IRepositoryAdditionalDatabases<Department> repositoryDepartments, IRepositoryAdditionalDatabases<Building> repositoryBuildings, long chatId)
        {

            var typeApplication = repositoryTypeApplication.FindItem(id);

            var newapp = repositoryApplications.FindItem(clientStates[update.CallbackQuery.Message.Chat.Id].Value);

            if (newapp != null)
                repositoryApplications.UpdateTypeApp(newapp.ID, typeApplication);

            var command = new SubmitNewAppCommand(ouremployee, cancellationToken, botClient, repositoryApplications, repositoryEmployees,
                clientStates, repositoryTypeApplication, repositoryDepartments, repositoryBuildings, chatId, repositoryApplicationActions);

            await command.Execute(update);
        }

        public static async void UpdateBuildingApp(IRepositoryAdditionalDatabases<Building> repositoryBuildings, IApplicationRepository repositoryApplications,
            Update update, ITelegramBotClient botClient, CancellationToken cancellationToken, Dictionary<long, UserStates> clientStates, Employee ouremployee,
            int id, IRepositoryEmployees repositoryEmployees, IApplicationActionRepository repositoryApplicationActions,
            IRepositoryAdditionalDatabases<Department> repositoryDepartments, long chatId, IRepositoryAdditionalDatabases<TypeApplication> repositoryTypeApplication)
        {
            var building = repositoryBuildings.FindItem(id);

            var newapp = repositoryApplications.FindItem(clientStates[update.CallbackQuery.Message.Chat.Id].Value);

            if (newapp != null)
                repositoryApplications.UpdateBuildingApp(newapp.ID, building);

            var command = new SubmitNewAppCommand(ouremployee, cancellationToken, botClient, repositoryApplications, repositoryEmployees,
                clientStates, repositoryTypeApplication, repositoryDepartments, repositoryBuildings, chatId, repositoryApplicationActions);

            await command.Execute(update);

        }
    }
}
