using LibraryDAL.Repositories;

namespace LibraryHillelEF
{
    public class ReaderFunc
    {
        public async Task<string> GetAllPublisherTypes()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var publishrepos = new PublisherCodeTypeRepository(unitOfWork);
                var publishcode = await publishrepos.GetAll();
                string codes = string.Empty;
                foreach (var code in publishcode)
                {
                    codes += $"{code.PublisherCodeName}\n";
                }
                return codes;
            }
        }
        public async Task<string> GetAllBooksFree()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var bookrepos=new BookRepository(unitOfWork);
                var list = await bookrepos.GetAllFree();
                string history = "Full list of free book in library:\n";
                foreach (var item in list)
                    history += item.ToString() + '\n';
                return history;
            }
        }
        //public async Task<string> GetBookFreeByTitle()
        //{
        //    using (var unitOfWork = new UnitOfWork())
        //    {

        //    }
        //}
        //public async Task<string> GetBooksFreeByAuthor()
        //{
        //    using (var unitOfWork = new UnitOfWork())
        //    {

        //    }
        //}
        //public async Task<string> GetInfoAuthor()
        //{
        //    using (var unitOfWork = new UnitOfWork())
        //    {

        //    }
        //}
        //public async Task<string> GetAllTakenBooks()
        //{
        //    using (var unitOfWork = new UnitOfWork())
        //    {

        //    }
        //}
        //public async Task<string> TakeBook()
        //{
        //    using (var unitOfWork = new UnitOfWork())
        //    {

        //    }
        //}
    }
}
