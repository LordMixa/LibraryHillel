using LibraryDAL.Entities;
using LibraryDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryHillelEF
{
    public abstract class AddReader
    {
        public async Task AddNewReader(string firstName, string lastName,
            string typeOfDocument, string documentNumber, string login, string password, string email)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var docnumtyperepos = new DocumentTypeRepository(unitOfWork);
                var readerrepos = new ReaderRepository(unitOfWork);
                var docnumtype = await docnumtyperepos.GetByName(typeOfDocument);
                if (docnumtype != null)
                {
                    await readerrepos.Create(new Reader
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        TypeOfDocument = docnumtype,
                        DocumentNumber = documentNumber,
                        Email = email,
                        Password = password,
                        Login = login
                    });
                    await unitOfWork.SaveAsync();
                }
            }
        }
    }
}
