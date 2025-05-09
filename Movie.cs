namespace MovieLibrary.Models
{
    public class Movie
    {
        public string MovieID { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public string Availability => IsAvailable ? "Available" : "Borrowed";

        public bool IsAvailable { get; set; } = true;

        public override string ToString()
        {
            return $"{Title} ({ReleaseYear}) - {Director} [{Genre}] - {Availability}";
        }
    }
}