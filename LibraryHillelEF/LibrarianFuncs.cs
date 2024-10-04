using LibraryDAL.Entities;
using LibraryDAL.Repositories;
using System.Collections.Generic;
using System.Text;

namespace LibraryHillelEF
{
    public class LibrarianFuncs : BasicFuncs
    {
        public async Task<bool> AddNewReader(string firstName, string lastName,
    string typeOfDocument, string documentNumber, string login, string password, string email)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var docnumtyperepos = new DocumentTypeRepository(unitOfWork);
                var readerrepos = new ReaderRepository(unitOfWork);

                var docnumtype = await docnumtyperepos.GetByName(typeOfDocument);
                if (docnumtype == null)
                    return false;

                if (await readerrepos.GetByLogin(login) != null || await readerrepos.GetByEmail(email) != null)
                    return false;

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
                return true;
            }
        }
        public async Task<string> GetAllPublisherTypes()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var publishrepos = new PublisherCodeTypeRepository(unitOfWork);
                var publishcode = await publishrepos.GetAll();
                string codes = string.Empty;
                foreach (var code in publishcode)
                {
                    codes += $"{code.PublisherCodeName}\n";
                }
                return codes;
            }
        }
        public async Task AddBook(string title, int? yearOfPublication, string? country, string? city,
    List<int> authorsid, string publisherCodeTypebyname, string publisherCode)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var publishrepos = new PublisherCodeTypeRepository(unitOfWork);
                var code = await publishrepos.GetByName(publisherCodeTypebyname);

                if (code == null)
                {
                    Console.WriteLine("Publisher code type not found.");
                    return;
                }

                var bookrepos = new BookRepository(unitOfWork);
                if (await bookrepos.GetByTitle(title) != null)
                {
                    Console.WriteLine("A book with this title already exists.");
                    return;
                }

                var authors = new List<Author>();
                if (authorsid != null && authorsid.Any())
                {
                    var authrepos = new AuthorRepository(unitOfWork);
                    foreach (var id in authorsid)
                    {
                        var author = await authrepos.Get(id);
                        if (author != null)
                            authors.Add(author);
                        else
                            Console.WriteLine($"Author with ID {id} not found.");
                    }
                }

                var newBook = new Book
                {
                    Title = title,
                    Authors = authors,
                    City = city, 
                    Country = country, 
                    PublisherCode = publisherCode,
                    PublisherCodeType = code,
                    YearOfPublication = yearOfPublication
                };

                await bookrepos.Create(newBook);
                await unitOfWork.SaveAsync();

                Console.WriteLine("Book successfully added.");
            }
        }

        public async Task UpdateBook(string bookname, string title, int? yearOfPublication, string? country,
    string? city, List<int> authorsid, string publisherCodeType, string publisherCode)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var bookrepos = new BookRepository(unitOfWork);
                var book = await bookrepos.GetByTitle(bookname);

                if (book != null)
                {
                    if (!string.IsNullOrEmpty(city))
                        book.City = city;

                    if (!string.IsNullOrEmpty(country))
                        book.Country = country;

                    if (!string.IsNullOrEmpty(publisherCodeType))
                    {
                        var publishrepos = new PublisherCodeTypeRepository(unitOfWork);
                        var code = await publishrepos.GetByName(publisherCodeType);
                        if (code != null)
                            book.PublisherCodeType = code;
                    }
                    if (!string.IsNullOrEmpty(publisherCode))
                        book.PublisherCode = publisherCode;

                    if (authorsid != null && authorsid.Any())
                    {
                        var authrepos = new AuthorRepository(unitOfWork);
                        var authors = new List<Author>();

                        foreach (var id in authorsid)
                        {
                            var author = await authrepos.Get(id);
                            if (author != null)
                                authors.Add(author);
                        }

                        book.Authors = authors;
                    }

                    if (!string.IsNullOrEmpty(title))
                        book.Title = title;

                    if (yearOfPublication.HasValue)
                        book.YearOfPublication = yearOfPublication.Value;

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

                if (booksTitle != null && booksTitle.Count > 0)
                {
                    foreach (var title in booksTitle)
                    {
                        var book = await bookrepos.GetByTitle(title);
                        if (book != null)
                            books.Add(book);
                        else
                            Console.WriteLine($"Book '{title}' not found in the database."); 
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
                    if (!string.IsNullOrEmpty(firstName))
                        author.FirstName = firstName;
                    if (!string.IsNullOrEmpty(lastName))
                        author.LastName = lastName;
                    if (!string.IsNullOrEmpty(middleName))
                        author.MiddleName = middleName;

                    if (booksTitle != null && booksTitle.Count > 0)
                    {
                        var bookrepos = new BookRepository(unitOfWork);
                        var books = new List<Book>();

                        foreach (var title in booksTitle)
                        {
                            var book = await bookrepos.GetByTitle(title);
                            if (book != null)
                                books.Add(book);
                            else
                                Console.WriteLine($"Book '{title}' not found in the database."); 
                        }
                        if (books.Count > 0)
                            author.Books = books;
                    }

                    authorrepos.Update(author);
                    await unitOfWork.SaveAsync();
                }
                else
                    Console.WriteLine($"Author '{fname} {lname}' not found in the database.");
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
                    if (!string.IsNullOrEmpty(firstName))
                        reader.FirstName = firstName;
                    if (!string.IsNullOrEmpty(lastName))
                        reader.LastName = lastName;

                    if (!string.IsNullOrEmpty(typeOfDocument))
                    {
                        var docnumtyperepos = new DocumentTypeRepository(unitOfWork);
                        var docnumtype = await docnumtyperepos.GetByName(typeOfDocument);
                        if (docnumtype != null)
                            reader.TypeOfDocument = docnumtype;
                        else
                            Console.WriteLine($"Document type '{typeOfDocument}' not found.");
                    }

                    if (!string.IsNullOrEmpty(documentNumber))
                        reader.DocumentNumber = documentNumber;
                    if (!string.IsNullOrEmpty(email))
                        reader.Email = email;
                    if (!string.IsNullOrEmpty(login))
                        reader.Login = login;
                    if (!string.IsNullOrEmpty(password))
                        reader.Password = password;

                    readerrepos.Update(reader);
                    await unitOfWork.SaveAsync();
                }
                else
                    Console.WriteLine($"Reader '{fname} {lname}' not found in the database.");
            }
        }

        public async Task<bool> DeleteReader(string docNum)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var readerrepos = new ReaderRepository(unitOfWork);

                var reader = await readerrepos.GetByDocument(docNum);
                if (reader != null)
                {
                    await readerrepos.DeleteByDocument(docNum);
                    await unitOfWork.SaveAsync();
                    Console.WriteLine($"Reader with document number '{docNum}' has been successfully deleted.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"No reader found with document number '{docNum}'.");
                    return false;
                }
            }
        }
        public async Task<string> GetDebtorList()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var takenBookRepos = new TakenBookRepository(unitOfWork);
                var list = await takenBookRepos.GetDebtorList();

                if (list == null || !list.Any())
                    return "No debtors found.";

                var history = new StringBuilder("List of Debtors:\n");
                foreach (var item in list)
                    history.AppendLine(item.ToString());

                return history.ToString();
            }
        }
        public async Task<string> GetFullListReaderTaken()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var readerRepos = new ReaderRepository(unitOfWork);
                var list = await readerRepos.GetAllTakenBook();

                if (list == null || !list.Any())
                    return "No books have been taken.";

                var history = new StringBuilder("Full List of Taken Books:\n");
                foreach (var item in list)
                    history.AppendLine(item.GetBookTakenToString());

                return history.ToString();
            }
        }
        public async Task<string> GetReaderHistory(string fname, string lname)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var readerRepos = new ReaderRepository(unitOfWork);
                var reader = await readerRepos.GetAllTakenBookByReader(fname, lname);

                if (reader != null)
                    return $"Reader history of {fname} {lname}:\n{reader.GetHistoryOfTakenBookToString()}";

                return "Reader not found.";
            }
        }
        public async Task<string> GetReaderInfo()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var readerrepos = new ReaderRepository(unitOfWork);
                var readerlist = await readerrepos.GetAll();
                string readers = "All readers info:\n";
                foreach (var item in readerlist)
                    readers += item.ToString() + '\n';
                return readers;
            }
        }
        public async Task<bool> UpdateTakenBookPeroid(DateOnly date, string bookPublishCode)
        {
            if (date <= DateOnly.FromDateTime(DateTime.Now))
                return false;

            using (var unitOfWork = new UnitOfWork())
            {
                var takenbookrepos = new TakenBookRepository(unitOfWork);
                var book = await takenbookrepos.GetByPublishCode(bookPublishCode);

                if (book != null)
                {
                    book.LastDayOfRent = date;
                    takenbookrepos.Update(book);
                    await unitOfWork.SaveAsync();
                    return true;
                }
            }
            return false;
        }
        public async Task<string> GetAllAuthorsId()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var authorrepos = new AuthorRepository(unitOfWork);
                var list = await authorrepos.GetAll();

                if (list == null || !list.Any())
                    return "No authors found.";

                var history = new StringBuilder("All authors:\n");
                foreach (var item in list)
                {
                    history.AppendLine(item.ToStringId());
                }
                return history.ToString();
            }
        }

    }
}
