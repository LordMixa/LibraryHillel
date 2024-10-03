namespace LibraryDAL.Entities
{
    public class TakenBook
    {
        public int TakenBookId { get; set; }
        public required DateOnly FirstDayOfRent { get; set; }
        public required Reader Reader { get; set; }
        public int ReaderId { get; set; }
        public required Book Book { get; set; }
        public int BookId { get; set; }
        private DateOnly _lastDayOfRent;
        public required DateOnly LastDayOfRent
        {
            get => _lastDayOfRent;
            init => _lastDayOfRent = value != default ? value : FirstDayOfRent.AddDays(30);
        }
        public DateOnly? DayOfReturn { get; set; }
        public override string ToString()
        {
            string authors = string.Join(", ", Book.Authors?.Select(a => a.ToString()) ?? new List<string>());
            return $"Book = {Book.Title}, Author = {authors}, Reader = {Reader.ToStringName()}, " +
                $"Period of rent: {FirstDayOfRent.ToString()}-{LastDayOfRent.ToString()}, " +
                $"Day of return: {(DayOfReturn == default ? "not yet" : DayOfReturn.ToString())}";
        }
    }
}
