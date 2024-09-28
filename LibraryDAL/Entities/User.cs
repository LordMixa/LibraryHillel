using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDAL.Entities
{
    public abstract class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
