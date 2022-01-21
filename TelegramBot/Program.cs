namespace TelegramBot
{
    public class Program
    {

        public static IRepositoryEmployees RepositoryEmployees = new RepositoryEmployeeSQL();
        
        public static IApplicationRepository RepositoryApplications = new ApplicationRepositorySQL();

        public static IApplicationActionRepository RepositoryApplicationActions = new ApplicationActionRepositorySQL();

        public static IRepositoryAdditionalDatabases<PositionEmployee> RepositoryPositions = new RepositoryAdditionalDatabasesSQL<PositionEmployee>();

        public static IRepositoryAdditionalDatabases<Department> RepositoryDepartment = new RepositoryAdditionalDatabasesSQL<Department>();

        public static IRepositoryAdditionalDatabases<Building> RepositoryBuildings = new RepositoryAdditionalDatabasesSQL<Building>();

        public static IRepositoryAdditionalDatabases<TypeApplication> RepositoryTypeApplication = new RepositoryAdditionalDatabasesSQL<TypeApplication>();
       
        public static IRepositoryAdditionalDatabases<ApplicationState> RepositoryApplicationState = new RepositoryAdditionalDatabasesSQL<ApplicationState>();

        static void Main(string[] args)
        {

            var bot = new Bot();
            
            //AddItem();

            bot.InitBot();

        }

        private static void AddItem()
        {
            RepositoryPositions.AddItem(new PositionEmployee(1, "Медицинский сотрудник"));
            RepositoryPositions.AddItem(new PositionEmployee(2, "Общебольничный персонал"));
            RepositoryPositions.AddItem(new PositionEmployee(3, "Научный сотрудник"));
            RepositoryPositions.AddItem(new PositionEmployee(4, "Медицинский инженер"));

            RepositoryDepartment.AddItem(new Department(1, "Администрация"));
            RepositoryDepartment.AddItem(new Department(2, "Поликлиника"));
            RepositoryDepartment.AddItem(new Department(3, "Клиника"));
            RepositoryDepartment.AddItem(new Department(4, "Наука"));
            RepositoryDepartment.AddItem(new Department(5, "Диагностика"));
            RepositoryDepartment.AddItem(new Department(6, "Кафедра"));

            RepositoryBuildings.AddItem(new Building(1, "2.1"));
            RepositoryBuildings.AddItem(new Building(2, "2.2"));
            RepositoryBuildings.AddItem(new Building(3, "2.3"));
            RepositoryBuildings.AddItem(new Building(4, "3"));
            RepositoryBuildings.AddItem(new Building(5, "5"));
            RepositoryBuildings.AddItem(new Building(6, "7"));

            RepositoryTypeApplication.AddItem(new TypeApplication(1, "Ремонт техники"));
            RepositoryTypeApplication.AddItem(new TypeApplication(2, "Проблемы с сетью"));
            RepositoryTypeApplication.AddItem(new TypeApplication(3, "Проблемы с МИС"));
            RepositoryTypeApplication.AddItem(new TypeApplication(4, "Просто спросить/прочее"));

            RepositoryApplicationState.AddItem(new ApplicationState(1, "подана"));
            RepositoryApplicationState.AddItem(new ApplicationState(2, "взята в работу"));
            RepositoryApplicationState.AddItem(new ApplicationState(3, "отклонена"));
            RepositoryApplicationState.AddItem(new ApplicationState(4, "исполнена"));


        }
    }
}
