using LibraryDAL.Entities;
using Microsoft.EntityFrameworkCore;

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
            Author? author = await _authorSet.FindAsync(id);
            if (author != null)
                _authorSet.Remove(author);
        }

        public async Task<Author?> Get(int id)
        {
            return await _authorSet.FirstOrDefaultAsync(x => x.AuthorId == id);
        }
        public async Task<Author?> GetByName(string fname, string lname)
        {
            return await _authorSet
                .Include(e=>e.Books)
                .FirstOrDefaultAsync(x => x.LastName == lname && x.FirstName == fname);
        }
        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _authorSet.ToListAsync();
        }

        public void Update(Author item)
        {
           _authorSet.Update(item);
        }
    }
}
