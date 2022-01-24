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
        public List<Employee> FindTechEmployee();
        public Employee FindItem(int id);
        public Employee FindItemChatID(long chatID);
        public int FindState(long chatID);
        public void AddNewEmployee(long chatID);
        public void ChangeState(long chatID, int state);
        public void UpdateFIOEmployee(long chatID, string fio);
        public void UpdatePositionEmployee(long chatID, PositionEmployee position);
        public void UpdateDepartmentEmployee(long chatID, Department department);
        public void UpdateIsExecutorEmployee(long chatID, bool isexecutor);
        public List<Employee> GetListEmployee();
    }

}

