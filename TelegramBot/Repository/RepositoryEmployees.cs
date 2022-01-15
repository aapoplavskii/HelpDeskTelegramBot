using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class RepositoryEmployees
    {
        private List<Employee> _employees = new List<Employee>();

        public Employee FindNameItem(string name) => _employees.FirstOrDefault(s => s.FIO.Contains(name));


        public Employee FindItem(int id) => _employees.FirstOrDefault(s => s.Id == id);
        public Employee FindItemChatID(long chatID) => _employees.FirstOrDefault(s => s.Chat_ID == chatID);

        public int FindState(long chatID) => _employees.FirstOrDefault(s => s.Chat_ID == chatID).State;

        public void AddNewEmployee(long chatID)
        {
            _employees.Add(new Employee(chatID));
        }

        public void ChangeState(Employee employee, int state)
        {
            employee.State = state;
        }

        public void UpdateFIOEmployee(long chatID, int state, string fio)
        {
            var employee = FindItemChatID(chatID);

            employee.FIO = fio;
            employee.State = state;

        }
        public void UpdatePositionEmployee(long chatID, int state, PositionEmployee position)
        {
            var employee = FindItemChatID(chatID);

            employee.Position = position;
            employee.State = state;


        }
    }
}
