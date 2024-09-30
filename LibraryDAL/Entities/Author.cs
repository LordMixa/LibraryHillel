using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDAL.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ?MiddleName { get; set; }
        public DateOnly ?Birthday { get; set; }
        public List<Book> ?Books { get; set;  }
        public override string ToString()
        {
            return $"{FirstName} {LastName} {MiddleName}";
        }
    }
}
