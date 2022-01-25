using LinqToDB;
using System.Collections.Generic;
using System.Linq;

namespace TelegramBot
{
    public class RepositoryEmployeeSQL : IRepositoryEmployees
    {
        public void AddNewEmployee(long chatID)
        {
            var newemployee = new Employee(chatID);

            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                var table = db.Insert(newemployee);
            }

        }

        public void ChangeState(long chatID, int state)
        {
            var employee = FindItemChatID(chatID);

            employee.State = state;

            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                var table = db.Update(employee);
            }
        }

        public Employee FindItem(int id)
        {
            Employee item = null;
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                item = db.GetTable<Employee>().FirstOrDefault(x => x.ID == id);
            }

            return item;
        }

        public Employee FindItemChatID(long chatID)
        {
            Employee item = null;
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                item = db.GetTable<Employee>().FirstOrDefault(x => x.Chat_ID == chatID);

            }

            return item;
        }

        public Employee FindNameItem(string name)
        {
            Employee item = null;
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                item = db.GetTable<Employee>().FirstOrDefault(x => x.FIO.Contains(name));
            }

            return item;
        }

        public int FindState(long chatID)
        {
            int state = 0;
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                state = db.GetTable<Employee>().FirstOrDefault(x => x.Chat_ID == chatID).State;
            }

            return state;
        }

        public List<Employee> FindTechEmployee()
        {
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {

                var table = db.GetTable<Employee>().Where(x => x.IsExecutor).Select(s => s);
                var list = table.ToList();
                return list;


            }
        }

        public List<Employee> GetListEmployee()
        {
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {

                var table = db.GetTable<Employee>();
                var list = table.ToList();
                return list;


            }
        }

        public void UpdateDepartmentEmployee(long chatID, Department department)
        {
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                var employee = FindItemChatID(chatID);
                employee.DepartmentID = department.ID;

                var table = db.Update(employee);

            }
        }

        public void UpdateFIOEmployee(long chatID, string fio)
        {
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                var employee = FindItemChatID(chatID);
                employee.FIO = fio;
                var table = db.Update(employee);

            }
        }

        public void UpdateIsExecutorEmployee(long chatID, bool isexecutor)
        {
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                var employee = FindItemChatID(chatID);
                employee.IsExecutor = isexecutor;

                var table = db.Update(employee);

            }
        }

        public void UpdatePositionEmployee(long chatID, PositionEmployee position)
        {
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                var employee = FindItemChatID(chatID);
                employee.PositionEmployeeID = position.ID;

                var table = db.Update(employee);

            }
        }
    }
}
