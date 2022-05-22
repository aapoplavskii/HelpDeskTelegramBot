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
        public Application AddNewApp(long chatID, IRepositoryEmployees repositoryEmployees);
        public void ChangeState(int appID, int state);
        public void UpdateTypeApp(int appID, TypeApplication typeApplication);
        public void UpdateBuildingApp(int appID, Building building);
        public void UpdateRoomApp(int appID, string room);
        public void UpdatePhoneApp(int appID, string phone);
        public void UpdateContentApp(int appID, string content);
        public List<Application> GetListApp();

    }
}
