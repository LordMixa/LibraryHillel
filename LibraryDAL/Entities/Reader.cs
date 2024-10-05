namespace LibraryDAL.Entities
{
    public class Reader : User
    {
        public int ReaderId { get; set; }
        public List<TakenBook>? TakenBook { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DocumentType TypeOfDocument{ get; set; }
        public required string DocumentNumber { get; set; }
        public override string ToString()
        {
            return $"{FirstName} {LastName}, Document^ {TypeOfDocument.DocumentTypeName}: {DocumentNumber}, Account: Email: {Email}, Login: {Login}, Password: {Password}";
        }
        public string ToStringName()
        {
            return $"{FirstName} {LastName}";
        }
        public string GetBookTakenToString()
        {
            string books = string.Join(", ", TakenBook?
                .Where(x => x.Book != null && x.DayOfReturn == null)
                .Select(x => x.Book.Title)!);
            return $"Reader: {FirstName} {LastName}\nBooks: {books}";
        }
        public string GetHistoryOfTakenBookToString()
        {
            int countdebt = 0;
            if (TakenBook != null)
            {
                foreach (var item in TakenBook)
                {
                    if(item.DayOfReturn == null && item.LastDayOfRent < DateOnly.FromDateTime(DateTime.Now))
                        countdebt++;
                }
            }
            string books = string.Join("\n", TakenBook?
                .Select(x => x.ToString())!);
            return $"{string.Join(", ", books) ?? "No taken book"}, Count of debt: {countdebt}";
        }
    }
}
