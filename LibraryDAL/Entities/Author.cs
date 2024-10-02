namespace LibraryDAL.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string ?MiddleName { get; set; }
        public DateOnly ?Birthday { get; set; }
        public List<Book> ?Books { get; set;  }
        public override string ToString()
        {
            return $"{FirstName} {LastName} {MiddleName}";
        }
    }
}
