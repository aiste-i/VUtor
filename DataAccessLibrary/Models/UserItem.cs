using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class UserItem
    {
        [Key]
        public int Id { get; set; }
        public string ProfileId { get; set; }
        public ProfileEntity Profile { get; set; }
    }
}
