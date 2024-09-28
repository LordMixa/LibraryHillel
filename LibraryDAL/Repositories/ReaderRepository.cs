using LibraryDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Reader book = await _readerSet.FindAsync(id);
            if (book != null)
                _readerSet.Remove(book);
        }

        public async Task<Reader>? Get(int id)
        {
            return await _readerSet.FindAsync(id);
        }

        public async Task<IEnumerable<Reader>> GetAll()
        {
            return await _readerSet.ToListAsync();
        }

        public async Task Update(Reader item)
        {
            _readerSet.Update(item);
        }
    }
}
