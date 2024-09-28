using LibraryDAL.Entities;
using LibraryDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryHillelEF
{
    public class LibrarianFuncs
    {
        public async Task AddBook(string title, int? yearOfPublication, string? country, string? city, List<Author> authors, PublisherCodeType publisherCodeType, string PublisherCode)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var bookrepos = new BookRepository(unitOfWork);
                await bookrepos.Create(new Book { Title = title, Authors = authors, 
                    City = city, Country = country, 
                    PublisherCode = PublisherCode, PublisherCodeType = publisherCodeType, 
                    YearOfPublication = yearOfPublication });
                await unitOfWork.SaveAsync();
            }
            
        }
        //public void UpdateBook(Book book, )
        //{
        //    using (var unitOfWork = new UnitOfWork())
        //    {
        //        var bookrepos = new BookRepository(unitOfWork);
        //        await bookrepos.Create(new Book
        //        {
        //            Title = title,
        //            Authors = authors,
        //            City = city,
        //            Country = country,
        //            PublisherCode = PublisherCode,
        //            PublisherCodeType = publisherCodeType,
        //            YearOfPublication = yearOfPublication
        //        });
        //        await unitOfWork.SaveAsync();
        //    }
        //}
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
        public void UpdateAuthor()
        {

        }
        public void AddReader()
        {

        }
        public void UpdateReader()
        {

        }
        public void DeleteReader()
        {

        }
        public void GetHistoryTakenBook()
        {

        }
        public void GetDebtorList()
        {

        }
        public void GetFullListReaderTaken()
        {

        }
        public void GetFullListBookTaken()
        {

        }
        public void GetReaderHistory()
        {

        }
    }
}
