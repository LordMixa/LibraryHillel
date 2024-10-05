using LibraryDAL.Entities;
using LibraryDAL.Repositories;
using System.Collections.Generic;

namespace LibraryHillelEF
{
    public class ReaderFunc : BasicFuncs
    {
        public string _firstname;
        public string _lastname;
        public ReaderFunc(string firstname, string lastname)
        {
            _firstname = firstname;
            _lastname = lastname;
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
                else return "Reader not found."; ;
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

                if (reader != null)
                {
                    if (book != null)
                    {
                        if (book.IsFree())
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
                            Console.WriteLine("Book taken successfully.");
                        }
                        else
                            Console.WriteLine("The book is not available for rent.");
                    }
                    else
                        Console.WriteLine("Book not found.");
                }
                else
                    Console.WriteLine("Reader not found.");
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

                if (reader != null)
                {
                    if (takenbook != null)
                    {
                        if (takenbook.Reader == reader && takenbook.DayOfReturn == null)
                        {
                            takenbook.DayOfReturn = DateOnly.FromDateTime(DateTime.Now);
                            takenbookrepos.Update(takenbook);
                            await unitOfWork.SaveAsync();
                            Console.WriteLine("Book returned successfully.");
                        }
                        else
                            Console.WriteLine("The book has already been returned or was not borrowed by this reader.");
                    }
                    else
                        Console.WriteLine("Book not found.");
                }
                else
                    Console.WriteLine("Reader not found.");
            }
        }
    }
}
