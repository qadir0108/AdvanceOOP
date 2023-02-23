using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Logic
{
    public class BusinessLogic
    {
        private readonly MvcMovieContext _context;

        public void Run()
        {
            // 1. Where
            List<Movie> moviesThatStartsWithHorror = _context.Movie.Where(x => x.Title.StartsWith("horror")).ToList();

            // 2. OrderBy
            List<Movie> moviesOrderedById = _context.Movie.OrderBy(x => x.Id).ToList();

            // Take
            _context.Movie.Take(1);

            // Skip
            _context.Movie.Skip(2).Take(1);

            // 3. Find
            Movie movie = _context.Movie.Find(1);

            // 4. First
            Movie firstMovieWithPriceGreaterThan100 = _context.Movie.First(x => x.Price > 100);

            // 5. FirstOrDefault
            Movie firstOrDefaultMovieWithPriceGreaterThan100 = _context.Movie.FirstOrDefault(x => x.Price > 100);

            // Count
            int allMoviesCount = _context.Movie.Count();

            // Min
            decimal minPrice = _context.Movie.Min(x => x.Price);

            // Sum
            decimal sumPrice = _context.Movie.Sum(x => x.Price);

            // GroupBy
            var groupsByGenre = _context.Movie.GroupBy(x => x.Genre);

        }
    }
}
