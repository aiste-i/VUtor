using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models.User.UserItems
{
    public class ItemEntity
    {
        public Guid? Id { get; set; }
        public Guid? CreatorId { get; set; }
    }
}
