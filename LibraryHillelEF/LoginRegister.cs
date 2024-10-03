using LibraryDAL.Entities;
using LibraryDAL.Repositories;

namespace LibraryHillelEF
{
    internal class LoginRegister : AddReader
    {
        public string LoginUser(string login, string password)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var reposread = new ReaderRepository(unitOfWork);
                var reposlib = new LibrarianRepository(unitOfWork);
                List<User> users = new List<User>();

                var listlib = reposlib.GetAll();
                var listread = reposread.GetAll();
                
                foreach (var item in listlib.Result)
                    users.Add(item);
                foreach (var item in listread.Result)
                    users.Add(item);
                var user = users.Find(u => u.Login == login && u.Password == password);
                if (user != null)
                {
                    if (user is Librarian)
                        return "lib";
                    else if (user is Reader)
                        return "read";
                    else
                        return "error";
                }
                else return "error";
            }
        }

        public async Task AddNewLibrarian(string login, string password, string email)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var reposlib = new LibrarianRepository(unitOfWork);
                await reposlib.Create(new Librarian
                {
                    Email = email,
                    Password = password,
                    Login = login
                });
                await unitOfWork.SaveAsync();
            }
        }
    }
}
