﻿using LibraryDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task Delete(int id)
        {
            Book book = await _bookSet.FindAsync(id);
            if (book != null)
                _bookSet.Remove(book);
        }

        public async Task<Book>? Get(int id)
        {
            return await _bookSet.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _bookSet.ToListAsync();
        }

        public async Task Update(Book item)
        {
            _bookSet.Update(item);
        }
    }
}
