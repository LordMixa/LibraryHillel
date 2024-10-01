using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDAL.Entities
{
    public class Reader : User
    {
        public int ReaderId { get; set; }
        public List<TakenBook> TakenBook { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DocumentType TypeOfDocument{ get; set; }
        public string DocumentNumber { get; set; }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
        public string GetBookTakenToString()
        {
            string books = string.Join(", ", TakenBook
                .Where(x => x.DayOfReturn == null)
                .Select(x => x.Book.ToString()));
            return $"{FirstName} {LastName}\nBooks: {books}";
        }
        public string GetHistoryOfTakenBookToString()
        {
            //string takenbooks = string.Join(", ", TakenBook.ToString());
            return $"{string.Join(", ", TakenBook.ToString())}";
        }
    }
}
