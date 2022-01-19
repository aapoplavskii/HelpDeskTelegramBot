using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public interface IApplicationRepository
    {
        public Application FindItem(int id);
        public Application AddNewApp(Employee employee);
        public void ChangeState(Application application, int state);
        public void UpdateTypeApp(Application app, TypeApplication typeApplication, int state);
        public void UpdateBuildingApp(Application app, Building building, int state);
        public void UpdateRoomApp(Application app, string room);
        public void UpdatePhoneApp(Application app, string phone);
        public void UpdateContentApp(Application app, string content);
        public List<Application> GetListApp();

    }
}
