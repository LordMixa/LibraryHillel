using LibraryDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryDAL.Repositories
{
    public class ReaderRepository : IRepository<Reader>
    {
        public readonly DbSet<Reader> _readerSet;
        public readonly UnitOfWork _unitOfWork;
        public ReaderRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readerSet = _unitOfWork.Context.Set<Reader>();
        }
        public async Task Create(Reader item)
        {
            await _readerSet.AddAsync(item);
        }

        public async Task Delete(int id)
        {
            Reader? reader = await _readerSet.FindAsync(id);
            if (reader != null)
                _readerSet.Remove(reader);
        }
        public async Task DeleteByDocument(string doc)
        {
            Reader? reader = await _readerSet.FirstOrDefaultAsync(e => e.DocumentNumber == doc);
            if (reader != null)
                _readerSet.Remove(reader);
        }
        public async Task<Reader?> Get(int id)
        {
            return await _readerSet.FirstOrDefaultAsync(e => e.ReaderId==id);
        }
        public async Task<Reader?> GetByName(string fname, string lname)
        {
            return await _readerSet.FirstOrDefaultAsync(x => x.LastName == lname && x.FirstName == fname);
        }
        public async Task<IEnumerable<Reader>> GetAll()
        {
            return await _readerSet.Include(r => r.TypeOfDocument)!.ToListAsync();
        }
        public async Task<IEnumerable<Reader>> GetAllTakenBook()
        {
            return await _readerSet
                .Include(r => r.TakenBook)!
                .ThenInclude(r => r.Book)
                .Where(x => x.TakenBook != null)
                .ToListAsync();
        }
        public async Task<Reader?> GetAllTakenBookByReader(string fname, string lname)
        {
            return await _readerSet
                .Include(r => r.TakenBook)!
                .ThenInclude(r => r.Book)
                .ThenInclude(r => r.Authors)
                .FirstOrDefaultAsync(x => x.LastName == lname && x.FirstName == fname);
        }
        public void Update(Reader item)
        {
            _readerSet.Update(item);
        }
    }
}
