using LibraryDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryDAL.Repositories
{
    public class PublisherCodeTypeRepository : IRepository<PublisherCodeType>
    {
        public readonly DbSet<PublisherCodeType> _publishCodeTypeSet;
        public readonly UnitOfWork _unitOfWork;
        public PublisherCodeTypeRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _publishCodeTypeSet = _unitOfWork.Context.Set<PublisherCodeType>();
        }
        public async Task Create(PublisherCodeType item)
        {
            await _publishCodeTypeSet.AddAsync(item);
        }

        public async Task Delete(int id)
        {
            PublisherCodeType? publishCodeType = await _publishCodeTypeSet.FindAsync(id);
            if (publishCodeType != null)
                _publishCodeTypeSet.Remove(publishCodeType);
        }
        public async Task<PublisherCodeType?> GetByName(string name)
        {
            return await _publishCodeTypeSet.FirstOrDefaultAsync(x => x.PublisherCodeName==name);
        }
        public async Task<PublisherCodeType?> Get(int id)
        {
            return await _publishCodeTypeSet.FindAsync(id);
        }

        public async Task<IEnumerable<PublisherCodeType>> GetAll()
        {
            return await _publishCodeTypeSet.ToListAsync();
        }

        public void Update(PublisherCodeType item)
        {
            _publishCodeTypeSet.Update(item);
        }
    }
}
