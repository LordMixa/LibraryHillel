using LibraryDAL;
using LibraryDAL.Entities;
using LibraryDAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryHillelEF
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            LibraryContext context = new LibraryContext();
            LibrarianFuncs librarianFuncs = new LibrarianFuncs();
            //await librarianFuncs.AddAuthor("Albert", "Einstein", "Genius", new DateOnly(1900, 1, 1), null);
            //await librarianFuncs.UpdateAuthor("Albert", "Einstein", "John", "Whick", null, null, null);
            //await librarianFuncs.AddBook("Book", 2004, null, null, new List<int> { 1 }, "ISBN", "123123123");
            //await librarianFuncs.AddReader("Michael", "Jordan", "Passport", "a1w1w1w1", "olololo", "hohohoho", "asd@gmail.com");
            //await librarianFuncs.UpdateReader("Michael", "Jordan", null, null, null, null, null, null, "update@gmail.com");
            //await librarianFuncs.DeleteReader("a1w1w1w1");
            Console.WriteLine(await librarianFuncs.GetDebtorList());


            //var strings = librarianFuncs.GetAllPublisherTypes().Result;
            //using (var unitOfWork = new UnitOfWork())
            //{
            //    var reposread = new ReaderRepository(unitOfWork);
            //    var repos = new TakenBookRepository(unitOfWork);
            //    var docrepos = new DocumentTypeRepository(unitOfWork);
            //    var reposbook = new BookRepository(unitOfWork);
            //    var book = new Book { Title = "testbook", YearOfPublication = 2004, Authors = new List<Author> { author }, PublisherCodeType = publishtype, PublisherCode = "123123123" };
            //    reposbook.Create(book);
            //    //context.Book.Add(book);
            //    //context.SaveChanges();
            //    unitOfWork.Save();
            //}
            //context.SaveChanges();
            //librarianFuncs.
            //using (var unitOfWork = new UnitOfWork())
            //{
            //    var reposuath = new AuthorRepository(unitOfWork);
            //    reposuath.Create(new Author { FirstName = "Cool author", LastName = "cool author lastname", MiddleName = "Cooooooool" });
            //    unitOfWork.Save();
            //}
            //using (var unitOfWork = new UnitOfWork())
            //{
            //    var publishrepos = new PublisherCodeTypeRepository(unitOfWork);
            //    var pubcode = publishrepos.Get(1).Result;
            //    var reposbook = new BookRepository(unitOfWork);
            //    var reposuath = new AuthorRepository(unitOfWork);
            //    Author authorw = reposuath.Get(1).Result;
            //    var book = new Book { City = "Paris", Country = "France", PublisherCodeType = pubcode, PublisherCode = "231313131", Title = "very Cool book", Authors = new List<Author> { author }, };
            //    reposbook.Create(book);
            //    unitOfWork.Save();
            //}
            //using (var unitOfWork = new UnitOfWork())
            //{
            //    var reposread = new ReaderRepository(unitOfWork);
            //    var repos = new TakenBookRepository(unitOfWork);
            //    var docrepos = new DocumentTypeRepository(unitOfWork);
            //    var doctype = docrepos.Get(1).Result;
            //    reposread.Create(new Reader { Login = "user5", Password = "password5", Email = "user5@example.com", FirstName = "Cool reader", LastName = "Veru coool", TypeOfDocument = doctype, DocumentNumber = "asdasdada" });
            //    unitOfWork.Save();
            //}
            //using (var unitOfWork = new UnitOfWork())
            //{
            //    var reposread = new ReaderRepository(unitOfWork);
            //    var repos = new TakenBookRepository(unitOfWork);
            //    var reposbook = new BookRepository(unitOfWork);
            //    var book = await reposbook.Get(1);

            //    var read = await reposread.Get(13);
            //    //Console.WriteLine(unitOfWork.Context.Entry(read).State);

            //    await repos.Create(new TakenBook { DayOfReturn = null, FirstDayOfRent = new DateOnly(2024, 03, 20), 
            //        LastDayOfRent = new DateOnly(2024, 09, 20), Book = book, Reader = read });
            //    unitOfWork.Save();
            //}
            //context.DocumentType.Add(new DocumentType { DocumentTypeName = "Passport" });
            //context.DocumentType.Add(new DocumentType { DocumentTypeName = "Student ticket" });
            //context.PublisherCodeType.Add(new PublisherCodeType { PublisherCodeName = "ISBN" });
            //context.PublisherCodeType.Add(new PublisherCodeType { PublisherCodeName = "BBK" });
            //context.SaveChanges();

            //var logreg = new LoginRegister();
            //string type = logreg.LoginUser("user1", "password1");
            //context.DocumentType.Add(new DocumentType { DocumentTypeName = "Passport" });
            //context.SaveChanges();

            //List<User> listusers = new List<User>
            //{
            //    new Librarian { Login = "user1", Password = "password1", Email = "user1@example.com" },
            //    new Librarian { Login = "user2", Password = "password2", Email = "user2@example.com" },
            //    new Librarian { Login = "user3", Password = "password3", Email = "user3@example.com" },
            //    new Librarian { Login = "user4", Password = "password4", Email = "user4@example.com" },
            //    new Reader { Login = "user5", Password = "password5", Email = "user5@example.com" },
            //    new Reader { Login = "user6", Password = "password6", Email = "user6@example.com" },
            //    new Reader { Login = "user7", Password = "password7", Email = "user7@example.com" },
            //    new Reader { Login = "user8", Password = "password8", Email = "user8@example.com" },
            //    new Reader { Login = "user9", Password = "password9", Email = "user9@example.com" },
            //    new Reader { Login = "user10", Password = "password10", Email = "user10@example.com" }
            //};
            //DocumentType type = context.DocumentType.Find(1);
            //context.Reader.Add(new Reader
            //{
            //    Login = "user1",
            //    Password = "password1",
            //    Email = "user1@example.com",
            //    FirstName = "John",
            //    LastName = "Doe",
            //    TypeOfDocument = type,
            //    DocumentNumber = "123456"
            //});
            //context.SaveChanges();

            //context.DocumentType.Add(new DocumentType { DocumentTypeName = "Passport" });
        }
    }
}
