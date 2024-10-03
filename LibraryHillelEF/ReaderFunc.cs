using LibraryDAL.Entities;
using LibraryDAL.Repositories;
using System.Collections.Generic;

namespace LibraryHillelEF
{
    public class ReaderFunc
    {
        public string _firstname;
        public string _lastname;
        public ReaderFunc(string firstname, string lastname)
        {
            _firstname = firstname;
            _lastname = lastname;
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
        public async Task<string> GetAllBooksFree()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var bookrepos = new BookRepository(unitOfWork);
                var list = await bookrepos.GetAllFree();
                string history = "Full list of free book in library:\n";
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
        public async Task<string> GetAllTakenBooks()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var readerrepos = new ReaderRepository(unitOfWork);
                var reader = await readerrepos.GetAllTakenBookByReader(_firstname, _lastname);
                if (reader != null)
                {
                    if (reader.TakenBook != null)
                    {
                        reader.TakenBook = reader.TakenBook
                            .OrderBy(book => book.LastDayOfRent < DateOnly.FromDateTime(DateTime.Now) ? 0 : 1)
                            .ThenBy(book => book.LastDayOfRent)
                            .ToList();
                    }
                    return reader.GetHistoryOfTakenBookToString();
                }
                else return "Error";
            }
        }
        public async Task TakeBook(string publishcode)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var readerrepos = new ReaderRepository(unitOfWork);
                var bookrepos = new BookRepository(unitOfWork);
                var reader = await readerrepos.GetByName(_firstname, _lastname);
                var book = await bookrepos.GetByPublishCode(publishcode);
                if(reader != null)
                {
                    if (book != null && book.IsFree())
                    {
                        var takenbookrepos = new TakenBookRepository(unitOfWork);
                        var takenBook = new TakenBook
                        {
                            Book = book,
                            Reader = reader,
                            FirstDayOfRent = DateOnly.FromDateTime(DateTime.Now),
                            LastDayOfRent = default
                        };
                        await takenbookrepos.Create(takenBook);
                        await unitOfWork.SaveAsync();
                    }
                }
            }
        }
        public async Task ReturnBook(string publishcode)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var readerrepos = new ReaderRepository(unitOfWork);
                var reader = await readerrepos.GetByName(_firstname, _lastname);
                var takenbookrepos = new TakenBookRepository(unitOfWork);
                var takenbook = await takenbookrepos.GetByPublishCode(publishcode);
                if(takenbook != null && takenbook.Reader == reader && takenbook.DayOfReturn == null)
                {
                    takenbook.DayOfReturn = DateOnly.FromDateTime(DateTime.Now);
                    takenbookrepos.Update(takenbook);
                    await unitOfWork.SaveAsync();
                }
            }
        }
    }
}
