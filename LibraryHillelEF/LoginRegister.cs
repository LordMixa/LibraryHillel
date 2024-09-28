using LibraryDAL;
using LibraryDAL.Entities;
using LibraryDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryHillelEF
{
    internal class LoginRegister
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
    }
}
