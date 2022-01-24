using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class RepositoryEmployees: IRepositoryEmployees
    {
        private List<Employee> _employees = new List<Employee>();

        public Employee FindNameItem(string name) => _employees.FirstOrDefault(s => s.FIO.Contains(name));


        public Employee FindItem(int id) => _employees.FirstOrDefault(s => s.ID == id);
        public Employee FindItemChatID(long chatID) => _employees.FirstOrDefault(s => s.Chat_ID == chatID);

        public int FindState(long chatID) => _employees.FirstOrDefault(s => s.Chat_ID == chatID).State;

        public void AddNewEmployee(long chatID)
        {
            _employees.Add(new Employee(chatID));
        }

        public void ChangeState(long chatID, int state)
        {
            var employee = FindItemChatID(chatID);

            employee.State = state;
        }

        public void UpdateFIOEmployee(long chatID, string fio)
        {
            var employee = FindItemChatID(chatID);
            if (employee != null)
            {
                employee.FIO = fio;
            }

        }
        public void UpdatePositionEmployee(long chatID, PositionEmployee position)
        {
            var employee = FindItemChatID(chatID);

            if (employee != null)
            {
                employee.Position = position;
                employee.PositionEmployeeID = position.ID;
            }

        }
        public void UpdateDepartmentEmployee(long chatID, Department department)
        {
            var employee = FindItemChatID(chatID);

            if (employee != null)
            {
                employee.Department = department;
                employee.DepartmentID = department.ID;
            }

        }

        public void UpdateIsExecutorEmployee(long chatID, bool isexecutor)
        {
            var employee = FindItemChatID(chatID);
            if (employee != null)
            {
                employee.IsExecutor = isexecutor;
            }
        }

        public List<Employee> GetListEmployee() => _employees;

        public List<Employee> FindTechEmployee() => _employees.Where(x => x.IsExecutor).Select(s => s).ToList(); 
       
    }
}
