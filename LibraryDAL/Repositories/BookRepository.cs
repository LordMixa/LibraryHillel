using LibraryDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryDAL.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        public readonly DbSet<Book> _bookSet;
        public readonly UnitOfWork _unitOfWork;
        public BookRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _bookSet = _unitOfWork.Context.Set<Book>();
        }
        public async Task Create(Book item)
        {
            await _bookSet.AddAsync(item);
        }
        public async Task<Book?> GetByTitle(string title)
        {
            return await _bookSet.FirstOrDefaultAsync(x => x.Title == title);
        }
        public async Task Delete(int id)
        {
            Book? book = await _bookSet.FindAsync(id);
            if (book != null)
                _bookSet.Remove(book);
        }

        public async Task<Book?> Get(int id)
        {
            return await _bookSet.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _bookSet.ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetAllFree()
        {
            return await _bookSet.Include(e => e.TakenBook)
                .Include(e => e.Authors)
                .Where(e => e.TakenBook == null || e.TakenBook.All(tb => tb.DayOfReturn != null))
                .ToListAsync();
        }
        public async Task<Book?> GetFreeByTitle(string title)
        {
            return await _bookSet.Include(e => e.TakenBook)
                .Include(e => e.Authors)
                .Where(e => e.TakenBook == null || e.TakenBook.All(tb => tb.DayOfReturn != null))
                .FirstOrDefaultAsync(x => x.Title == title);
        }
        public async Task<List<Book>> GetFreeByAuthor(string fname, string lname)
        {
            return await _bookSet
                .Include(e => e.TakenBook)
                .Include(e => e.Authors)
                .Where(e => (e.TakenBook == null || e.TakenBook.All(tb => tb.DayOfReturn != null))
                        && (e.Authors != null && e.Authors.Any(a => a.FirstName == fname && a.LastName == lname)))
                .ToListAsync();
        }
        public async Task<Book?> GetByPublishCode(string code)
        {
            return await _bookSet.Include(e => e.TakenBook).FirstOrDefaultAsync(x => x.PublisherCode == code);
        }
        public void Update(Book item)
        {
            _bookSet.Update(item);
        }
    }
}
