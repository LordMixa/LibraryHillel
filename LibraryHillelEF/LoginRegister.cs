using LibraryDAL.Entities;
using LibraryDAL.Repositories;

namespace LibraryHillelEF
{
    internal class LoginRegister : BasicFuncs
    {
        public async Task<User?> LoginUser(string login, string password)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var librarianRepository = new LibrarianRepository(unitOfWork);
                var librarian = await librarianRepository.GetByLogPas(login, password);

                if (librarian != null)
                    return librarian;

                var readerRepository = new ReaderRepository(unitOfWork);
                var reader = await readerRepository.GetByLogPas(login, password);

                return reader;
            }
        }

        public async Task<bool> AddNewLibrarian(string login, string password, string email)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var reposlib = new LibrarianRepository(unitOfWork);

                if (await reposlib.GetByLogin(login) != null || await reposlib.GetByEmail(email) != null)
                {
                    return false;
                }

                await reposlib.Create(new Librarian
                {
                    Email = email,
                    Password = password,
                    Login = login
                });
                await unitOfWork.SaveAsync();

                return true; 
            }
        }

        public async Task<bool> AddNewReader(string firstName, string lastName,
    string typeOfDocument, string documentNumber, string login, string password, string email)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var docnumtyperepos = new DocumentTypeRepository(unitOfWork);
                var readerrepos = new ReaderRepository(unitOfWork);

                var docnumtype = await docnumtyperepos.GetByName(typeOfDocument);
                if (docnumtype == null)
                    return false; 

                if (await readerrepos.GetByLogin(login) != null || await readerrepos.GetByEmail(email) != null)
                    return false;

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
                return true; 
            }
        }
    }
}
