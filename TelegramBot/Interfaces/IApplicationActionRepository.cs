using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public interface IApplicationActionRepository
    {
        public ApplicationAction AddNewAppAction(int appID, int employeeID);
        public void ChangeState(ApplicationAction applicationAction, ApplicationState state);

        public void SetDate(ApplicationAction applicationAction, DateTime dateTime);

        public List<ApplicationAction> GetListApp();

    }
}
