using LibraryDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryDAL.Repositories
{
    public class TakenBookRepository : IRepository<TakenBook>
    {
        public readonly DbSet<TakenBook> _takenBookSet;
        public readonly UnitOfWork _unitOfWork;
        public TakenBookRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _takenBookSet = _unitOfWork.Context.Set<TakenBook>();
        }
        public async Task Create(TakenBook item)
        {
            await _takenBookSet.AddAsync(item);
        }

        public async Task Delete(int id)
        {
            TakenBook? takenBook = await _takenBookSet.FindAsync(id);
            if (takenBook != null)
                _takenBookSet.Remove(takenBook);
        }

        public async Task<TakenBook?> Get(int id)
        {
            return await _takenBookSet.FindAsync(id);
        }
        public async Task<TakenBook?> GetByPublishCode(string code)
        {
            return await _takenBookSet.FirstOrDefaultAsync(x => x.Book.PublisherCode == code);
        }
        public async Task<IEnumerable<TakenBook>?> GetDebtorList()
        {
            return await _takenBookSet
                .Include(tb => tb.Book)
                .Include(tb => tb.Reader)
                .Where(e => e.DayOfReturn == null && e.LastDayOfRent < DateOnly.FromDateTime(DateTime.Now))
                .ToListAsync();
        }
        public async Task<IEnumerable<TakenBook>> GetAll()
        {
            return await _takenBookSet
                .Include(tb => tb.Book)
                .Include(tb => tb.Reader).ToListAsync();
        }

        public void Update(TakenBook item)
        {
            _takenBookSet.Update(item);
        }
    }
}
