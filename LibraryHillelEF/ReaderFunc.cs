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
    }
}
