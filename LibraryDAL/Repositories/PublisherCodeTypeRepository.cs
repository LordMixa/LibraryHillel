using LibraryDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDAL.Repositories
{
    public class PublisherCodeTypeRepository : IRepository<PublisherCodeType>
    {
        public readonly DbSet<PublisherCodeType> _documentTypeSet;
        public readonly UnitOfWork _unitOfWork;
        public PublisherCodeTypeRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _documentTypeSet = _unitOfWork.Context.Set<PublisherCodeType>();
        }
        public async Task Create(PublisherCodeType item)
        {
            await _documentTypeSet.AddAsync(item);
        }

        public async Task Delete(int id)
        {
            PublisherCodeType publishCodeType = await _documentTypeSet.FindAsync(id);
            if (publishCodeType != null)
                _documentTypeSet.Remove(publishCodeType);
        }
        public async Task<PublisherCodeType?> GetByName(string name)
        {
            return await _documentTypeSet.FirstOrDefaultAsync(x => x.PublisherCodeName==name);
        }
        public async Task<PublisherCodeType?> Get(int id)
        {
            return await _documentTypeSet.FindAsync(id);
        }

        public async Task<IEnumerable<PublisherCodeType>> GetAll()
        {
            return await _documentTypeSet.ToListAsync();
        }

        public async Task Update(PublisherCodeType item)
        {
            _documentTypeSet.Update(item);
        }
    }
}
