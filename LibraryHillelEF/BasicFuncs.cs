using LibraryDAL.Entities;
using LibraryDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryHillelEF
{
    public abstract class BasicFuncs
    {
        public async Task<string> GetAllBooksFree()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var bookrepos = new BookRepository(unitOfWork);
                var list = await bookrepos.GetAllFree();

                if (list == null || !list.Any())
                    return "No free books available in the library.";

                string history = "Full list of free books in the library:\n";
                foreach (var item in list)
                    history += item.ToString() + '\n';

                return history;
            }
        }
        public async Task<string> GetBookFreeByTitle(string title)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var bookrepos = new BookRepository(unitOfWork);
                var book = await bookrepos.GetFreeByTitle(title);
                if (book != null)
                    return book.ToString();
                else
                    return "Book not found";
            }
        }
        public async Task<string> GetBooksFreeByAuthor(string fname, string lname)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var bookrepos = new BookRepository(unitOfWork);
                var books = await bookrepos.GetFreeByAuthor(fname, lname);
                string history = $"List of free book in of {fname} {lname}:\n";
                foreach (var item in books)
                    history += item.ToString() + '\n';
                return history;
            }
        }
        public async Task<string> GetInfoAuthor(string fname, string lname)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var authrepos = new AuthorRepository(unitOfWork);
                var author = await authrepos.GetByName(fname, lname);
                if (author != null)
                    return author.GetInfo();
                else
                    return "Author not found";
            }
        }
    }
}
