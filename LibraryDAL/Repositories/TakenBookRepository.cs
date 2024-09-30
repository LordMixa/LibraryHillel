﻿using LibraryDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            TakenBook takenBook = await _takenBookSet.FindAsync(id);
            if (takenBook != null)
                _takenBookSet.Remove(takenBook);
        }

        public async Task<TakenBook>? Get(int id)
        {
            return await _takenBookSet.FindAsync(id);
        }

        public async Task<IEnumerable<TakenBook>> GetAll()
        {
            return await _takenBookSet.ToListAsync();
        }

        public async Task Update(TakenBook item)
        {
            _takenBookSet.Update(item);
        }
    }
}
