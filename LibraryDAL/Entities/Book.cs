using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDAL.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public TakenBook ?TakenBook { get; set; }
        public List<Author> Authors { get; set; }
        public PublisherCodeType PublisherCodeType { get; set; }
        public string PublisherCode { get; set; }
        public int ?YearOfPublication { get; set; }
        public string ?Country { get; set; }
        public string ?City { get; set; }
    }
}
