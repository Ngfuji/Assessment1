using System.Collections;
using System.Collections.Generic;
using MovieLibrary.Models;

namespace MovieLibrary.Services
{
    public class MovieLibraryService
    {
        private readonly Hashtable movieTable = new Hashtable();
        private readonly LinkedList<Movie> movieList = new LinkedList<Movie>();
        private readonly Dictionary<string, Queue<string>> waitingQueues = new Dictionary<string, Queue<string>>();

        // Add a new movie
        public bool AddMovie(Movie movie)
        {
            if (movieTable.ContainsKey(movie.MovieID))
                return false;

            movieList.AddLast(movie);
            movieTable[movie.MovieID] = movie;
            waitingQueues[movie.MovieID] = new Queue<string>();
            return true;
        }

        // Retrieve movie by ID
        public Movie? GetMovieByID(string movieID)
        {
            return movieTable[movieID] as Movie;
        }

        // Return all movies
        public IEnumerable<Movie> GetAllMovies()
        {
            return movieList;
        }

        // Borrow a movie
        public bool BorrowMovie(string movieID, string userName)
        {
            var movie = GetMovieByID(movieID);
            if (movie == null) return false;

            if (movie.IsAvailable)
            {
                movie.IsAvailable = false;
                return true;
            }

            waitingQueues[movieID].Enqueue(userName);
            return false;
        }

        // Return a movie
        public string? ReturnMovie(string movieID)
        {
            var movie = GetMovieByID(movieID);
            if (movie == null) return null;

            if (waitingQueues[movieID].Count > 0)
            {
                return waitingQueues[movieID].Dequeue(); // assign to next user
            }

            movie.IsAvailable = true;
            return null;
        }

        // Get waitlist for a movie
        public Queue<string>? GetWaitingList(string movieID)
        {
            return waitingQueues.ContainsKey(movieID) ? waitingQueues[movieID] : null;
        }
    }
}
