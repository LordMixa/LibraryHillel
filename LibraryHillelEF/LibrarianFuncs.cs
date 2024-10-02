using LibraryDAL.Entities;
using LibraryDAL.Repositories;
using System.Collections.Generic;

namespace LibraryHillelEF
{
    public class LibrarianFuncs
    {
        public async Task AddBook(string title, int? yearOfPublication, string? country, string? city, 
            List<int> authorsid, string publisherCodeTypebyname, string PublisherCode)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var publishrepos = new PublisherCodeTypeRepository(unitOfWork);
                var code = await publishrepos.GetByName(publisherCodeTypebyname);
                if (code != null)
                {
                    var authrepos = new AuthorRepository(unitOfWork);
                    var authors = new List<Author>();
                    if (authorsid != null)
                    {
                        foreach (var item in authorsid)
                        {
                            var author = await authrepos.Get(item);
                            if (author != null)
                                authors.Add(author);
                        }
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
        }
        public async Task UpdateBook(string bookname, string title, int? yearOfPublication, string? country, 
            string? city, List<Author> authors, string publisherCodeType, string publisherCode)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var bookrepos = new BookRepository(unitOfWork);
                var book = await bookrepos.GetByTitle(bookname);
                if (book != null)
                {
                    if (city != null)
                        book.City = city;
                    if (country != null)
                        book.Country = country;
                    if (publisherCode != null)
                    {
                        var publishrepos = new PublisherCodeTypeRepository(unitOfWork);
                        var code = await publishrepos.GetByName(publisherCodeType);
                        if(code != null) 
                            book.PublisherCodeType = code;
                    }
                    if (publisherCode != null)
                        book.PublisherCode = publisherCode;
                    if (authors != null)
                        book.Authors = authors;
                    if (title != null)
                        book.Title = title;
                    if (yearOfPublication != null)
                        book.YearOfPublication = yearOfPublication;
                    bookrepos.Update(book);
                    await unitOfWork.SaveAsync();
                }
            }
        }
        public async Task AddAuthor(string firstName, string lastName, string? middleName, 
            DateOnly? birthday, List<string>? booksTitle)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var authorrepos = new AuthorRepository(unitOfWork);
                var bookrepos = new BookRepository(unitOfWork);
                var books = new List<Book>();
                if (booksTitle != null)
                {
                    foreach (var item in booksTitle)
                    {
                        var book = await bookrepos.GetByTitle(item);
                        if (book != null)
                            books.Add(book);
                    }
                }
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
        public async Task UpdateAuthor(string fname, string lname, string firstName, 
            string lastName, string? middleName, DateOnly? birthday, List<string>? booksTitle)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var authorrepos = new AuthorRepository(unitOfWork);
                var author = await authorrepos.GetByName(fname, lname);
                if (author != null)
                {
                    if (birthday != null)
                        author.Birthday = birthday;
                    if (firstName != null)
                        author.FirstName = firstName;
                    if (lastName != null)
                        author.LastName = lastName;
                    if (middleName != null)
                        author.MiddleName = middleName;
                    if (booksTitle != null)
                    {
                        var bookrepos = new BookRepository(unitOfWork);
                        var books = new List<Book>();
                        if (booksTitle != null)
                        {
                            foreach (var item in booksTitle)
                            {
                                var book = await bookrepos.GetByTitle(item);
                                if (book != null)
                                    books.Add(book);
                            }
                        }
                        author.Books = books;
                    }
                    authorrepos.Update(author);
                    await unitOfWork.SaveAsync();
                }
            }
        }
        public async Task AddReader(string firstName, string lastName, 
            string typeOfDocument, string documentNumber, string login, string password, string email)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var docnumtyperepos = new DocumentTypeRepository(unitOfWork);
                var readerrepos = new ReaderRepository(unitOfWork);
                var docnumtype = await docnumtyperepos.GetByName(typeOfDocument);
                if (docnumtype != null)
                {                    
                    await readerrepos.Create(new Reader
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        TypeOfDocument = docnumtype,
                        DocumentNumber = documentNumber,
                        Email = email,
                        Password = password,
                        Login = login
                    });
                    await unitOfWork.SaveAsync();
                }
            }
        }
        public async Task UpdateReader(string fname, string lname, string firstName, string lastName, 
            string typeOfDocument, string documentNumber, string login, string password, string email)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var readerrepos = new ReaderRepository(unitOfWork);
                var reader = await readerrepos.GetByName(fname, lname);
                if (reader != null)
                {
                    if (firstName != null)
                        reader.FirstName = firstName;
                    if (lastName != null)
                        reader.LastName = lastName;
                    if (typeOfDocument != null)
                    {
                        var docnumtyperepos = new DocumentTypeRepository(unitOfWork);
                        var docnumtype = await docnumtyperepos.GetByName(typeOfDocument);
                        if (docnumtype != null)
                            reader.TypeOfDocument = docnumtype;
                    }
                    if (documentNumber != null)
                        reader.DocumentNumber = documentNumber;
                    if (email != null)
                        reader.Email = email;
                    if (login != null)
                        reader.Login = login;
                    if (password != null)
                        reader.Password = password;

                    readerrepos.Update(reader);
                    await unitOfWork.SaveAsync();
                }
            }
        }
        public async Task DeleteReader(string docNum)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var readerrepos = new ReaderRepository(unitOfWork);
                await readerrepos.DeleteByDocument(docNum);
                await unitOfWork.SaveAsync();
            }
        }
        public async Task<string> GetHistoryTakenBook()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var bookrepos = new BookRepository(unitOfWork);
                var takenbookrepos = new TakenBookRepository(unitOfWork);
                var list = await takenbookrepos.GetAll();
                string history=string.Empty;
                foreach (var item in list)
                    history += item.ToString() + '\n';
                return history;
            }
        }
        public async Task<string> GetDebtorList()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var takenbookrepos = new TakenBookRepository(unitOfWork);
                var list = await takenbookrepos.GetDebtorList()!;
                string history = string.Empty;
                foreach (var item in list)
                    history += item.ToString() + '\n';
                return history;
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
