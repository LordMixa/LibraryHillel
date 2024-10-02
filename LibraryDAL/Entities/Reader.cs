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
            return $"{FirstName} {LastName}";
        }
        public string GetBookTakenToString()
        {
            string books = string.Join(", ", TakenBook?
                .Where(x => x.DayOfReturn == null && x.Book != null)
                .Select(x => x.Book.ToString()) ?? new List<string>());
            return $"{FirstName} {LastName}\nBooks: {books}";
        }
        public string GetHistoryOfTakenBookToString()
        {
            return $"{string.Join(", ", TakenBook?.ToString())??"No taken book"}";
        }
    }
}
