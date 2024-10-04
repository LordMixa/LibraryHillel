using LibraryDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace LibraryHillelEF
{
    public class ProgramMenu
    {
        public async Task MainMenu()
        {
            while (true)
            {
                Console.WriteLine("Type 'login' to log in, 'register' to register, or 'exit' to quit:");
                string check = Console.ReadLine()!;

                if (check == "login")
                {
                    await LoginMenu();
                    break;
                }
                else if (check == "register")
                {
                    await RegisterMenu();
                    break;
                }
                else if (check == "exit")
                {
                    Console.WriteLine("Exiting the application.");
                    break;
                }
                else
                    Console.WriteLine("Incorrect data");
            }
        }
        private async Task RegisterMenu()
        {
            Console.WriteLine("Select registration type:");
            Console.WriteLine("1 - Register Librarian");
            Console.WriteLine("2 - Register Reader");
            string choice = Console.ReadLine()!;

            switch (choice)
            {
                case "1":
                    await RegisterLibrarian();
                    break;
                case "2":
                    await RegisterReader();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select 1 or 2.");
                    break;
            }
        }
        private async Task RegisterLibrarian()
        {
            var logreg = new LoginRegister();
            Console.WriteLine("Enter login:");
            string login = Console.ReadLine()!;

            Console.WriteLine("Enter password:");
            string password = Console.ReadLine()!;

            Console.WriteLine("Enter email:");
            string email = Console.ReadLine()!;

            var result = await logreg.AddNewLibrarian(login, password, email);

            if (result)
                Console.WriteLine("Librarian registered successfully.");
            else
                Console.WriteLine("Failed to register librarian. Login or email might already be in use.");
        }
        private async Task RegisterReader()
        {
            var logreg = new LoginRegister();
            Console.WriteLine("Enter first name:");
            string firstName = Console.ReadLine()!;

            Console.WriteLine("Enter last name:");
            string lastName = Console.ReadLine()!;

            Console.WriteLine("Enter type of document:");
            string typeOfDocument = Console.ReadLine()!;

            Console.WriteLine("Enter document number:");
            string documentNumber = Console.ReadLine()!;

            Console.WriteLine("Enter login:");
            string login = Console.ReadLine()!;

            Console.WriteLine("Enter password:");
            string password = Console.ReadLine()!;

            Console.WriteLine("Enter email:");
            string email = Console.ReadLine()!;

            var result = await logreg.AddNewReader(firstName, lastName, typeOfDocument, documentNumber, login, password, email);

            if (result)
                Console.WriteLine("Reader registered successfully.");
            else
                Console.WriteLine("Failed to register reader. Document type might be invalid or login/email already in use.");
        }
        public async Task LoginMenu()
        {
            Console.WriteLine("Enter your login:");
            string log = Console.ReadLine()!;
            Console.WriteLine("Enter your password:");
            string pas = Console.ReadLine()!;

            LoginRegister loginRegister = new LoginRegister();

            var user = await loginRegister.LoginUser(log, pas);

            if (user is Librarian)
            {
                await LibrarianFuncsMenu();
            }
            else if (user is Reader)
            {
                await ReaderFuncMenu((Reader)user);
            }
            else
                Console.WriteLine("Incorrect data");
        }
        private async Task ReaderFuncMenu(Reader reader)
        {
            ReaderFunc readerFunc = new ReaderFunc(reader.FirstName, reader.LastName);
            Console.WriteLine($"Welcome {reader.FirstName} {reader.LastName}!");

            while (true)
            {
                Console.WriteLine("Choose:\n" +
                                  "1 - Get all books\n" +
                                  "2 - Search a book by Title\n" +
                                  "3 - Search a book by Author\n" +
                                  "4 - Get Info about Author\n" +
                                  "5 - Get All Taken Books\n" +
                                  "6 - Take a new Book\n" +
                                  "7 - Return a Book\n" +
                                  "8 - Exit");
                string check = Console.ReadLine()!;

                switch (check)
                {
                    case "1":
                        Console.WriteLine(await readerFunc.GetAllBooksFree());
                        break;

                    case "2":
                        Console.WriteLine("Enter the title of the book:");
                        string title = Console.ReadLine()!;
                        Console.WriteLine(await readerFunc.GetBookFreeByTitle(title));
                        break;

                    case "3":
                        Console.WriteLine("Enter the First Name of the Author:");
                        string fname = Console.ReadLine()!;
                        Console.WriteLine("Enter the Last Name of the Author:");
                        string lname = Console.ReadLine()!;
                        Console.WriteLine(await readerFunc.GetBooksFreeByAuthor(fname, lname));
                        break;

                    case "4":
                        Console.WriteLine("Enter the First Name of the Author:");
                        fname = Console.ReadLine()!;
                        Console.WriteLine("Enter the Last Name of the Author:");
                        lname = Console.ReadLine()!;
                        Console.WriteLine(await readerFunc.GetInfoAuthor(fname, lname));
                        break;

                    case "5":
                        Console.WriteLine(await readerFunc.GetAllTakenBooks());
                        break;

                    case "6":
                        Console.WriteLine("Enter the Publish code of the book in the library:");
                        string publishCode = Console.ReadLine()!;
                        await readerFunc.TakeBook(publishCode);
                        Console.WriteLine("Book taken successfully.");
                        break;

                    case "7":
                        Console.WriteLine("Enter the Publish code of the taken book:");
                        publishCode = Console.ReadLine()!;
                        await readerFunc.ReturnBook(publishCode);
                        Console.WriteLine("Book returned successfully.");
                        break;

                    case "8":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid selection, please try again.");
                        break;
                }
            }
        }
        private async Task LibrarianFuncsMenu()
        {
            LibrarianFuncs librarianFuncs = new LibrarianFuncs();
            Console.WriteLine("Welcome Librarian!");

            while (true)
            {
                Console.WriteLine("Choose:\n" +
                                  "1 - Add a new book\n" +
                                  "2 - Update Book\n" +
                                  "3 - Add new Reader\n" +
                                  "4 - Update Reader\n" +
                                  "5 - Delete Reader\n" +
                                  "6 - Add author\n" +
                                  "7 - Get Debtor List\n" +
                                  "8 - Get Full List Reader Taken\n" +
                                  "9 - Get Reader History\n" +
                                  "10 - Get Reader Info\n" +
                                  "11 - Update TakenBook Period\n" +
                                  "12 - Get all books\n" +
                                  "13 - Search a book by Title\n" +
                                  "14 - Search a book by Author\n" +
                                  "15 - Get Info about Author\n" +
                                  "16 - Exit");

                string check = Console.ReadLine()!;
                switch (check)
                {
                    case "1":
                        await AddNewBook(librarianFuncs);
                        break;

                    case "2":
                        await UpdateBook(librarianFuncs);
                        break;

                    case "3":
                        await AddNewReader(librarianFuncs);
                        break;

                    case "4":
                        await UpdateReaderInfo(librarianFuncs);
                        break;

                    case "5":
                        await DeleteReaderInfo(librarianFuncs);
                        break;

                    case "6":
                        await AddAuthorInfo(librarianFuncs);
                        break;

                    case "7":
                        Console.WriteLine(await librarianFuncs.GetDebtorList());
                        break;

                    case "8":
                        Console.WriteLine(await librarianFuncs.GetFullListReaderTaken());
                        break;

                    case "9":
                        await GetReaderHistoryInput(librarianFuncs);
                        break;

                    case "10":
                        Console.WriteLine(await librarianFuncs.GetReaderInfo());
                        break;

                    case "11":
                        await UpdateRentalPeriod(librarianFuncs);
                        break;

                    case "12":
                        Console.WriteLine(await librarianFuncs.GetAllBooksFree());
                        break;

                    case "13":
                        Console.WriteLine("Enter the title of the book:");
                        string title = Console.ReadLine()!;
                        Console.WriteLine(await librarianFuncs.GetBookFreeByTitle(title));
                        break;

                    case "14":
                        Console.WriteLine("Enter the First Name of the Author:");
                        string fname = Console.ReadLine()!;
                        Console.WriteLine("Enter the Last Name of the Author:");
                        string lname = Console.ReadLine()!;
                        Console.WriteLine(await librarianFuncs.GetBooksFreeByAuthor(fname, lname));
                        break;

                    case "15":
                        Console.WriteLine("Enter the First Name of the Author:");
                        fname = Console.ReadLine()!;
                        Console.WriteLine("Enter the Last Name of the Author:");
                        lname = Console.ReadLine()!;
                        Console.WriteLine(await librarianFuncs.GetInfoAuthor(fname, lname));
                        break;

                    case "16":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid selection, please try again.");
                        break;
                }
            }
        }
        private async Task UpdateReaderInfo(LibrarianFuncs librarianFuncs)
        {
            Console.WriteLine("Enter the first name of the reader to update:");
            string oldFirstName = Console.ReadLine()!;

            Console.WriteLine("Enter the last name of the reader to update:");
            string oldLastName = Console.ReadLine()!;

            Console.WriteLine("Enter new First Name (leave empty to keep current):");
            string newFirstName = Console.ReadLine()!;

            Console.WriteLine("Enter new Last Name (leave empty to keep current):");
            string newLastName = Console.ReadLine()!;

            Console.WriteLine("Enter new Type of Document (leave empty to keep current):");
            string newTypeOfDocument = Console.ReadLine()!;

            Console.WriteLine("Enter new Document Number (leave empty to keep current):");
            string newDocumentNumber = Console.ReadLine()!;

            Console.WriteLine("Enter new Login (leave empty to keep current):");
            string newLogin = Console.ReadLine()!;

            Console.WriteLine("Enter new Password (leave empty to keep current):");
            string newPassword = Console.ReadLine()!;

            Console.WriteLine("Enter new Email (leave empty to keep current):");
            string newEmail = Console.ReadLine()!;

            await librarianFuncs.UpdateReader(oldFirstName, oldLastName, newFirstName, newLastName,
                                               newTypeOfDocument, newDocumentNumber, newLogin, newPassword, newEmail);

            Console.WriteLine("Reader information updated successfully.");
        }
        private async Task AddNewBook(LibrarianFuncs librarianFuncs)
        {
            Console.WriteLine("Enter book info");

            Console.WriteLine("Enter book Title:");
            string title = Console.ReadLine()!;

            Console.WriteLine("Enter Year of Publication:");
            string yearOfPublicationInput = Console.ReadLine()!;
            int? year = int.TryParse(yearOfPublicationInput, out int yearValue) ? yearValue : (int?)null;

            Console.WriteLine("Enter Country:");
            string country = Console.ReadLine()!;

            Console.WriteLine("Enter City:");
            string city = Console.ReadLine()!;

            Console.WriteLine("Available Authors:");
            Console.WriteLine(await librarianFuncs.GetAllAuthorsId());

            Console.WriteLine("Enter Author IDs (separate multiple IDs with a comma):");
            string authorIdsInput = Console.ReadLine()!;
            List<int> authorIds = authorIdsInput.Split(',')
                                                  .Select(id => int.TryParse(id.Trim(), out int authorId) ? authorId : 0)
                                                  .Where(id => id != 0)
                                                  .ToList();

            Console.WriteLine("Available Publisher Types:");
            Console.WriteLine(await librarianFuncs.GetAllPublisherTypes());

            Console.WriteLine("Enter Publish Type:");
            string publishType = Console.ReadLine()!;

            Console.WriteLine("Enter Publish Number:");
            string publishNumber = Console.ReadLine()!;

            await librarianFuncs.AddBook(title, year, country, city, authorIds, publishType, publishNumber);

            Console.WriteLine("Book added successfully.");
        }
        private async Task UpdateBook(LibrarianFuncs librarianFuncs)
        {
            Console.WriteLine("Enter the old book Title:");
            string oldTitle = Console.ReadLine()!;

            Console.WriteLine("Enter the new book Title:");
            string newTitle = Console.ReadLine()!;

            Console.WriteLine("Enter Year of Publication:");
            string yearOfPublicationInput = Console.ReadLine()!;
            int? year = int.TryParse(yearOfPublicationInput, out int yearValue) ? yearValue : (int?)null;

            Console.WriteLine("Enter Country:");
            string country = Console.ReadLine()!;

            Console.WriteLine("Enter City:");
            string city = Console.ReadLine()!;

            Console.WriteLine("Available Authors:");
            Console.WriteLine(await librarianFuncs.GetAllAuthorsId());

            Console.WriteLine("Enter Author IDs (separate multiple IDs with a comma):");
            string authorIdsInput = Console.ReadLine()!;
            List<int> authorIds = authorIdsInput.Split(',')
                                                 .Select(id => int.TryParse(id.Trim(), out int authorId) ? authorId : 0)
                                                 .Where(id => id != 0)
                                                 .ToList();

            Console.WriteLine("Available Publisher Types:");
            Console.WriteLine(await librarianFuncs.GetAllPublisherTypes());

            Console.WriteLine("Enter Publish Type:");
            string publishType = Console.ReadLine()!;

            Console.WriteLine("Enter Publish Number:");
            string publishNumber = Console.ReadLine()!;

            await librarianFuncs.UpdateBook(oldTitle, newTitle, year, country, city, authorIds, publishType, publishNumber);
            Console.WriteLine("Book updated successfully.");
        }
        private async Task AddNewReader(LibrarianFuncs librarianFuncs)
        {
            Console.WriteLine("Enter new Reader information:");

            Console.WriteLine("Enter First Name:");
            string firstName = Console.ReadLine()!;

            Console.WriteLine("Enter Last Name:");
            string lastName = Console.ReadLine()!;

            Console.WriteLine("Enter Type of Document:");
            string typeOfDocument = Console.ReadLine()!;

            Console.WriteLine("Enter Document Number:");
            string documentNumber = Console.ReadLine()!;

            Console.WriteLine("Enter Login:");
            string login = Console.ReadLine()!;

            Console.WriteLine("Enter Password (preferably hashed):");
            string password = Console.ReadLine()!;

            Console.WriteLine("Enter Email:");
            string email = Console.ReadLine()!;

            bool success = await librarianFuncs.AddNewReader(firstName, lastName, typeOfDocument, documentNumber, login, password, email);

            if (success)
                Console.WriteLine("Reader added successfully.");
            else
                Console.WriteLine("Failed to add reader. Please check the input and try again.");
        }
        private async Task DeleteReaderInfo(LibrarianFuncs librarianFuncs)
        {
            Console.WriteLine("Enter the document number of the reader to delete:");
            string documentNumber = Console.ReadLine()!;

            bool result = await librarianFuncs.DeleteReader(documentNumber);

            if (result)
                Console.WriteLine("Reader deleted successfully.");
            else
                Console.WriteLine("Failed to delete reader.");
        }
        private async Task AddAuthorInfo(LibrarianFuncs librarianFuncs)
        {
            Console.WriteLine("Enter author's first name:");
            string firstName = Console.ReadLine()!;

            Console.WriteLine("Enter author's last name:");
            string lastName = Console.ReadLine()!;

            Console.WriteLine("Enter author's middle name (optional):");
            string? middleName = Console.ReadLine();

            Console.WriteLine("Enter author's birthday (YYYY-MM-DD) (optional):");
            string? birthdayInput = Console.ReadLine();
            DateOnly? birthday = DateOnly.TryParse(birthdayInput, out DateOnly parsedBirthday) ? parsedBirthday : null;

            Console.WriteLine("Enter the titles of books written by the author (separate titles with a comma):");
            string booksInput = Console.ReadLine()!;
            List<string> booksTitle = booksInput.Split(',')
                                                .Select(title => title.Trim())
                                                .Where(title => !string.IsNullOrEmpty(title))
                                                .ToList();

            await librarianFuncs.AddAuthor(firstName, lastName, middleName, birthday, booksTitle);
            Console.WriteLine("Author added successfully.");
        }
        private async Task GetReaderHistoryInput(LibrarianFuncs librarianFuncs)
        {
            Console.WriteLine("Enter reader's first name:");
            string firstName = Console.ReadLine()!;

            Console.WriteLine("Enter reader's last name:");
            string lastName = Console.ReadLine()!;

            string history = await librarianFuncs.GetReaderHistory(firstName, lastName);
            Console.WriteLine(history);
        }
        private async Task UpdateRentalPeriod(LibrarianFuncs librarianFuncs)
        {
            Console.WriteLine("Enter the new return date (YYYY-MM-DD):");
            string dateInput = Console.ReadLine()!;

            if (DateOnly.TryParse(dateInput, out DateOnly newReturnDate))
            {
                Console.WriteLine("Enter the book publish code:");
                string bookPublishCode = Console.ReadLine()!;

                bool isUpdated = await librarianFuncs.UpdateTakenBookPeroid(newReturnDate, bookPublishCode);

                if (isUpdated)
                    Console.WriteLine("The rental period has been updated successfully.");
                else
                    Console.WriteLine("Failed to update rental period. Ensure the date is valid and the book exists.");
            }
            else
                Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
        }
    }
}
