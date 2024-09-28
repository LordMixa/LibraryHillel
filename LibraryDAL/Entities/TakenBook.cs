namespace LibraryDAL.Entities
{
    public class TakenBook
    {
        public int TakenBookId { get; set; }
        public DateOnly FirstDayOfRent { get; set; }
        public Reader Reader { get; set; }
        public Book Book { get; set; }
        public DateOnly LastDayOfRent { get; set; }
        public DateOnly ?DayOfReturn { get; set; }
    }
}
