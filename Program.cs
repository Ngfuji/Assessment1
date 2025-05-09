// Movie class
public class Movie
{
    public string MovieID { get; set; }
    public string Title { get; set; }
    public string Director { get; set; }
    public string Genre { get; set; }
    public int ReleaseYear { get; set; }
    public bool IsAvailable { get; set; } = true;
    public Queue<string> WaitingList { get; set; } = new Queue<string>();
}

// MovieLibrary class
public class MovieLibrary
{
    private LinkedList<Movie> movies = new LinkedList<Movie>();
    private Dictionary<string, Movie> movieLookup = new Dictionary<string, Movie>();

    public void AddMovie(Movie movie)
    {
        if (movieLookup.ContainsKey(movie.MovieID)) throw new Exception("Duplicate Movie ID");
        movies.AddLast(movie);
        movieLookup[movie.MovieID] = movie;
    }

    public Movie SearchByID(string id)
    {
        return movieLookup.ContainsKey(id) ? movieLookup[id] : null;
    }

    public List<Movie> GetAllMovies() => movies.ToList();

    public bool DeleteMovie(string movieID)
    {
        if (!movieLookup.TryGetValue(movieID, out var movie)) return false;
        movies.Remove(movie);
        return movieLookup.Remove(movieID);
    }

    public void UpdateMovie(string movieID, Movie updated)
    {
        if (!movieLookup.ContainsKey(movieID)) throw new Exception("Movie not found");
        var movie = movieLookup[movieID];
        movie.Title = updated.Title;
        movie.Director = updated.Director;
        movie.Genre = updated.Genre;
        movie.ReleaseYear = updated.ReleaseYear;
        movie.IsAvailable = updated.IsAvailable;
    }
}
