namespace LibraryDAL.Entities
{
    public abstract class User
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
    }
}
