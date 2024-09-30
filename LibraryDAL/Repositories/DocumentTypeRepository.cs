using LibraryDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDAL.Repositories
{
    public class DocumentTypeRepository : IRepository<DocumentType>
    {
        public readonly DbSet<DocumentType> _documentTypeSet;
        public readonly UnitOfWork _unitOfWork;
        public DocumentTypeRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _documentTypeSet = _unitOfWork.Context.Set<DocumentType>();
        }
        public async Task Create(DocumentType item)
        {
            await _documentTypeSet.AddAsync(item);
        }

        public async Task Delete(int id)
        {
            DocumentType documentType = await _documentTypeSet.FindAsync(id);
            if (documentType != null)
                _documentTypeSet.Remove(documentType);
        }

        public async Task<DocumentType>? Get(int id)
        {
            return await _documentTypeSet.FindAsync(id);
        }

        public async Task<IEnumerable<DocumentType>> GetAll()
        {
            return await _documentTypeSet.ToListAsync();
        }

        public async Task Update(DocumentType item)
        {
            _documentTypeSet.Update(item);
        }
    }
}
