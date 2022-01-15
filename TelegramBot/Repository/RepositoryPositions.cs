using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class RepositoryPositions
    {
        private List<PositionEmployee> _positionEmployees = new List<PositionEmployee>();

        public RepositoryPositions()
        {
            _positionEmployees.Add(new PositionEmployee(1, "Медицинский сотрудник"));
            _positionEmployees.Add(new PositionEmployee(2, "Сотрудник администрации"));
            _positionEmployees.Add(new PositionEmployee(3, "Научный сотрудник"));
            _positionEmployees.Add(new PositionEmployee(4, "Медицинский инженер"));


        }

        public PositionEmployee FindItem(int id) => _positionEmployees.FirstOrDefault(s => s.Id == id);

    }
}
