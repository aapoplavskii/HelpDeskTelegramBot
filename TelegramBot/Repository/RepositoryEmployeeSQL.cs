using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class RepositoryEmployeeSQL : IRepositoryEmployees
    {
        public void AddNewEmployee(long chatID)
        {
            var newemployee = new Employee(chatID);

            using (Config.db)
            {
                Config.db.Insert(newemployee);
            }

        }

        public void ChangeState(Employee employee, int state)
        {
            employee.State = state;
            
            using (Config.db)
            {
                var table = Config.db.Update(employee);
            }
        }

        public Employee FindItem(int id)
        {
            Employee item = null;
            using (Config.db)
            {
               item = Config.db.GetTable<Employee>().FirstOrDefault(x => x.ID == id);
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
            using (Config.db)
            {
                item = Config.db.GetTable<Employee>().FirstOrDefault(x => x.FIO.Contains(name));
            }

            return item;
        }

        public int FindState(long chatID)
        {
           int state = 0;
            using (Config.db)
            {
                state = Config.db.GetTable<Employee>().FirstOrDefault(x => x.Chat_ID == chatID).State;
            }

            return state;
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

        public void UpdateDepartmentEmployee(long chatID, int state, Department department)
        {
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                var employee = FindItemChatID(chatID);
                employee.Department = department;
                employee.State = state;

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

        public void UpdateIsExecutorEmployee(long chatID, int state, bool isexecutor)
        {
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                var employee = FindItemChatID(chatID);
                employee.IsExecutor = isexecutor;
                employee.State = state;

                var table = db.Update(employee);

            }
        }

        public void UpdatePositionEmployee(long chatID, int state, PositionEmployee position)
        {
            using (var db = new LinqToDB.Data.DataConnection(LinqToDB.ProviderName.PostgreSQL, Config.SqlConnectionString))
            {
                var employee = FindItemChatID(chatID);
                employee.Position = position;
                employee.State = state;

                var table = db.Update(employee);

            }
        }
    }
}
