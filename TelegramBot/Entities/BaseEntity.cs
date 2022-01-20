using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class BaseEntity
    {
        [PrimaryKey]
        [Column (Name = "ID")]
        public int Id { get; set; }

    }
}
