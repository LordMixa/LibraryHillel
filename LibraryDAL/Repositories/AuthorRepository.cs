using LibraryDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDAL.Repositories
{
    public class AuthorRepository : IRepository<Author>
    {
        public readonly DbSet<Author> _authorSet;
        public readonly UnitOfWork _unitOfWork;
        public AuthorRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _authorSet = _unitOfWork.Context.Set<Author>();
        }
        public async Task Create(Author item)
        {
            await _authorSet.AddAsync(item);
        }

        public async Task Delete(int id)
        {
            Author book = await _authorSet.FindAsync(id);
            if (book != null)
                _authorSet.Remove(book);
        }

        public async Task<Author>? Get(int id)
        {
            return await _authorSet.FindAsync(id);
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _authorSet.ToListAsync();
        }

        public async Task Update(Author item)
        {
            _authorSet.Update(item);
        }
    }
}
