using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class ApplicationRepository : IApplicationRepository
    {
        public List<Application> applications { get; set; } = new List<Application>();

        public Application FindItem(int id)
        {
            return applications.FirstOrDefault(s => s.Id == id);
        }
    }
}
