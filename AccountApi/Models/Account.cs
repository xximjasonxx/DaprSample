namespace AccountApi.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int[] MoviesWatched { get; set; }
    }
}
