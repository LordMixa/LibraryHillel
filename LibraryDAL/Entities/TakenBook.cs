namespace LibraryDAL.Entities
{
    public class TakenBook
    {
        public int TakenBookId { get; set; }
        public DateOnly FirstDayOfRent { get; set; }
        public required Reader Reader { get; set; }
        public int ReaderId { get; set; }
        public required Book Book { get; set; }
        public int BookId { get; set; }
        public DateOnly LastDayOfRent { get; set; }
        public DateOnly? DayOfReturn { get; set; }
        public override string ToString()
        {
            string authors = string.Join(", ", Book.Authors?.Select(a => a.ToString()) ?? new List<string>());
            return $"Book = {Book.Title}, Author = {authors}, Reader = {Reader.ToString()}, " +
                $"Period of rent: {FirstDayOfRent.ToString()}-{LastDayOfRent.ToString()}, " +
                $"Day of return: {DayOfReturn.ToString() ?? "not yet"}";
        }
    }
}
