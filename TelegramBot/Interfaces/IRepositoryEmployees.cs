using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public interface IRepositoryEmployees
    {
        public Employee FindNameItem(string name);
        public Employee FindItem(int id);
        public Employee FindItemChatID(long chatID);
        public int FindState(long chatID);
        public void AddNewEmployee(long chatID);
        public void ChangeState(Employee employee, int state);
        public void UpdateFIOEmployee(long chatID, string fio);
        public void UpdatePositionEmployee(long chatID, int state, PositionEmployee position);
        public void UpdateDepartmentEmployee(long chatID, int state, Department department);
        public void UpdateIsExecutorEmployee(long chatID, int state, bool isexecutor);
        public List<Employee> GetListEmployee();
    }

}

