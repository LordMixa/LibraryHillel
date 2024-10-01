using LibraryDAL.Entities;
using LibraryDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryHillelEF
{
    public class LibrarianFuncs
    {
        public async Task<string> GetAllPublisherTypes()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var publishrepos = new PublisherCodeTypeRepository(unitOfWork);
                var publishcode = await publishrepos.GetAll();
                string codes = string.Empty;
                foreach (var code in publishcode) {
                    codes += $"{code.PublisherCodeName}\n";
                }
                return codes;
            }
        }
        public async Task AddBook(string title, int? yearOfPublication, string? country, string? city, List<int> authorsid, string publisherCodeTypebyname, string PublisherCode)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var publishrepos = new PublisherCodeTypeRepository(unitOfWork);
                var code = await publishrepos.GetByName(publisherCodeTypebyname);
                var authrepos = new AuthorRepository(unitOfWork);
                var authors = new List<Author>();
                foreach (var item in authorsid)
                {
                    var author = await authrepos.Get(item);
                    if (author != null)
                        authors.Add(author);
                }
                var bookrepos = new BookRepository(unitOfWork);

                if (await bookrepos.GetByTitle(title) == null)
                {
                    await bookrepos.Create(new Book
                    {
                        Title = title,
                        Authors = authors,
                        City = city,
                        Country = country,
                        PublisherCode = PublisherCode,
                        PublisherCodeType = code,
                        YearOfPublication = yearOfPublication
                    });
                    await unitOfWork.SaveAsync();
                }
            }
        }
        public async Task UpdateBook(string bookname, string title, int? yearOfPublication, string? country, string? city, List<Author> authors, PublisherCodeType publisherCodeType, string PublisherCode)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var bookrepos = new BookRepository(unitOfWork);
                var book = bookrepos.GetByTitle(title).Result;
                if (book == null)
                {
                    var newbook = new Book
                    {
                        Title = title,
                        Authors = authors,
                        City = city,
                        Country = country,
                        PublisherCode = PublisherCode,
                        PublisherCodeType = publisherCodeType,
                        YearOfPublication = yearOfPublication
                    };
                    UpdateBookDB(book, newbook);
                }
            }
        }
        private async Task UpdateBookDB(Book oldbook, Book newchanges)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                if(newchanges.City != null)
                    oldbook.City = newchanges.City;
                if (newchanges.Country!= null)
                    oldbook.Country = newchanges.Country; 
                if (newchanges.TakenBook != null)
                    oldbook.TakenBook = newchanges.TakenBook;
                if (newchanges.PublisherCodeType != null)
                    oldbook.PublisherCodeType = newchanges.PublisherCodeType;
                if (newchanges.PublisherCode != null)
                    oldbook.PublisherCode = newchanges.PublisherCode;
                if (newchanges.Authors != null)
                    oldbook.Authors = newchanges.Authors;
                if (newchanges.Title != null)
                    oldbook.Title = newchanges.Title;
                if (newchanges.YearOfPublication != null)
                    oldbook.YearOfPublication = newchanges.YearOfPublication;
                var bookrepos = new BookRepository(unitOfWork);
                await bookrepos.Update(oldbook);
                await unitOfWork.SaveAsync();
            }
        }
        public async Task AddAuthor(string firstName, string lastName, string? middleName, DateOnly? birthday, List<Book>? books )
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var authorrepos = new AuthorRepository(unitOfWork);
                await authorrepos.Create(new Author
                {
                    FirstName = firstName,
                    LastName = lastName,
                    MiddleName = middleName,
                    Birthday = birthday,
                    Books = books
                });
                await unitOfWork.SaveAsync();
            }
        }
        public async Task UpdateAuthor(Author oldauthor, Author newchanges)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                if (newchanges.Birthday != null)
                    oldauthor.Birthday = newchanges.Birthday;
                if (newchanges.FirstName != null)
                    oldauthor.FirstName = newchanges.FirstName;
                if (newchanges.LastName != null)
                    oldauthor.LastName = newchanges.LastName;
                if (newchanges.MiddleName != null)
                    oldauthor.MiddleName = newchanges.MiddleName;
                if (newchanges.Books != null)
                    oldauthor.Books = newchanges.Books;
                
                var authorrepos = new AuthorRepository(unitOfWork);
                await authorrepos.Update(oldauthor);
                await unitOfWork.SaveAsync();
            }
        }
        public async Task AddReader(List<TakenBook> takenBook, string firstName, string lastName, DocumentType typeOfDocument, string documentNumber, string login, string password, string email)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var readerrepos = new ReaderRepository(unitOfWork);
                await readerrepos.Create(new Reader
                {
                    FirstName = firstName,
                    LastName = lastName,
                    TakenBook = takenBook,
                    TypeOfDocument = typeOfDocument,
                    DocumentNumber = documentNumber,
                    Email = email,
                    Password = password,
                    Login = login
                });
                await unitOfWork.SaveAsync();
            }
        }
        public async Task UpdateReader(Reader oldreader, Reader newchanges)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                if (newchanges.TakenBook != null)
                    oldreader.TakenBook = newchanges.TakenBook;
                if (newchanges.FirstName != null)
                    oldreader.FirstName = newchanges.FirstName;
                if (newchanges.LastName != null)
                    oldreader.LastName = newchanges.LastName;
                if (newchanges.TypeOfDocument != null)
                    oldreader.TypeOfDocument = newchanges.TypeOfDocument;
                if (newchanges.DocumentNumber != null)
                    oldreader.DocumentNumber = newchanges.DocumentNumber;
                if (newchanges.Email != null)
                    oldreader.Email = newchanges.Email;
                if (newchanges.Login != null)
                    oldreader.Login = newchanges.Login;
                if (newchanges.Password != null)
                    oldreader.Password = newchanges.Password;

                var readerrepos = new ReaderRepository(unitOfWork);
                await readerrepos.Update(oldreader);
                await unitOfWork.SaveAsync();
            }
        }
        public async Task DeleteReader(int id)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var readerrepos = new ReaderRepository(unitOfWork);
                await readerrepos.Delete(id);
            }
        }
        public async Task<List<string>> GetHistoryTakenBook()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var takenbookrepos = new TakenBookRepository(unitOfWork);
                var list = await takenbookrepos.GetAll();
                List<string> history = new List<string>();
                foreach (var item in list)
                    history.Add(item.ToString());
                return history;
            }
        }
        public async Task<IEnumerable<TakenBook>> GetDebtorList()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var takenbookrepos = new TakenBookRepository(unitOfWork);
                return takenbookrepos.GetDebtorList().Result;
            }
        }
        public void GetFullListReaderTaken()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var readerrepos = new ReaderRepository(unitOfWork);


            }
        }
        public void GetFullListBookTaken()
        {

        }
        public void GetReaderHistory()
        {

        }
    }
}
