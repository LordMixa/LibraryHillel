﻿using LibraryDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryDAL.Repositories
{
    public class LibrarianRepository : IRepository<Librarian>
    {
        public readonly DbSet<Librarian> _librarianSet;
        public readonly UnitOfWork _unitOfWork;
        public LibrarianRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _librarianSet = _unitOfWork.Context.Set<Librarian>();
        }
        public async Task Create(Librarian item)
        {
            await _librarianSet.AddAsync(item);
        }
        public async Task<Librarian?> GetByLogPas(string login, string pass)
        {
            return await _librarianSet.FirstOrDefaultAsync(x => x.Login == login && x.Password == pass);
        }
        public async Task Delete(int id)
        {
            Librarian? librarian = await _librarianSet.FindAsync(id);
            if (librarian != null)
                _librarianSet.Remove(librarian);
        }

        public async Task<Librarian?> Get(int id)
        {
            return await _librarianSet.FindAsync(id);
        }

        public async Task<IEnumerable<Librarian>> GetAll()
        {
            return await _librarianSet.ToListAsync();
        }

        public void Update(Librarian item)
        {
            _librarianSet.Update(item);
        }

        public async Task<Librarian?> GetByLogin(string login)
        {
            return await _librarianSet.FirstOrDefaultAsync(x => x.Login == login);
        }

        public async Task<Librarian?> GetByEmail(string email)
        {
            return await _librarianSet.FirstOrDefaultAsync(x => x.Login == email);
        }
    }
}
