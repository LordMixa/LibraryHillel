using LibraryDAL;
using LibraryDAL.Entities;

namespace LibraryHillelEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LibraryContext context = new LibraryContext();
            //context.DocumentType.Add(new DocumentType { DocumentTypeName = "Driver license" });
            //context.DocumentType.Add(new DocumentType { DocumentTypeName = "Student ticket" });
            //context.PublisherCodeType.Add(new PublisherCodeType { PublisherCodeName = "ISBN" });
            //context.PublisherCodeType.Add(new PublisherCodeType { PublisherCodeName = "BBK" });
            //context.SaveChanges();

            //var logreg = new LoginRegister();
            //string type = logreg.LoginUser("user1", "password1");
            //context.DocumentType.Add(new DocumentType { DocumentTypeName = "Passport" });
            //context.SaveChanges();

            //List<User> listusers = new List<User>
            //{
            //    new Librarian { Login = "user1", Password = "password1", Email = "user1@example.com" },
            //    new Librarian { Login = "user2", Password = "password2", Email = "user2@example.com" },
            //    new Librarian { Login = "user3", Password = "password3", Email = "user3@example.com" },
            //    new Librarian { Login = "user4", Password = "password4", Email = "user4@example.com" },
            //    new Reader { Login = "user5", Password = "password5", Email = "user5@example.com" },
            //    new Reader { Login = "user6", Password = "password6", Email = "user6@example.com" },
            //    new Reader { Login = "user7", Password = "password7", Email = "user7@example.com" },
            //    new Reader { Login = "user8", Password = "password8", Email = "user8@example.com" },
            //    new Reader { Login = "user9", Password = "password9", Email = "user9@example.com" },
            //    new Reader { Login = "user10", Password = "password10", Email = "user10@example.com" }
            //};
            //DocumentType type = context.DocumentType.Find(1);
            //context.Reader.Add(new Reader
            //{
            //    Login = "user1",
            //    Password = "password1",
            //    Email = "user1@example.com",
            //    FirstName = "John",
            //    LastName = "Doe",
            //    TypeOfDocument = type,
            //    DocumentNumber = "123456"
            //});
            //context.SaveChanges();

            //context.DocumentType.Add(new DocumentType { DocumentTypeName = "Passport" });
        }
    }
}
