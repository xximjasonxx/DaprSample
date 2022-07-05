using System.Text.Json.Serialization;

namespace MovieApi.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre {  get; set; }

    }
}
