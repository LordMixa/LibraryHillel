namespace LibraryDAL.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public required string Title { get; set; }
        public List<Author> ?Authors { get; set; }
        public List<TakenBook>? TakenBook { get; set; }
        public required PublisherCodeType PublisherCodeType { get; set; }
        public required string PublisherCode { get; set; }
        public int ?YearOfPublication { get; set; }
        public string ?Country { get; set; }
        public string ?City { get; set; }
        public override string ToString()
        {
            string authors = string.Join(", ", Authors?.Select(x => x.ToString()) ?? new List<string>());
            return $"{Title}. Authors: {authors}";
        }
    }
}
