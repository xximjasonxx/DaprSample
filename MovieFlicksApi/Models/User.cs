namespace MovieFlicksApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public List<Movie> MovieWatched { get; set; }
    }
}