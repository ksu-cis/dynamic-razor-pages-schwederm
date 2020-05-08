using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// The movies to display on the index page
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }

        /// <summary>
        /// The current search terms
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string SearchTerms { get; set; }

        /// <summary>
        /// The filtered MPAA Ratings
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// The filtered genres
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string[] Genres { get; set; }

        /// <summary>
        /// The minimum IMDB Rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? IMDBMin { get; set; }

        /// <summary>
        /// The maximum IMDB Rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? IMDBMax { get; set; }

        /// <summary>
        /// The minimum Rotten Tomatoes Rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? RottenTomatoesMin { get; set; }

        /// <summary>
        /// The maximum Rotten Tomatoes Rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public int? RottenTomatoesMax { get; set; }

        /// <summary>
        /// Does the response initialization for incoming GET requests
        /// </summary>
        public void OnGet()
        {
            /*
            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenre(Movies, Genres);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.FilterByRottenTomatoesRating(Movies, RottenTomatoesMin, RottenTomatoesMax);
            */
            Movies = MovieDatabase.All;
            // Search movie titles for the SearchTerms
            if (SearchTerms != null)
            {
                Movies = Movies.Where(movie => 
                    movie.Title != null && 
                    movie.Title.Contains(SearchTerms, StringComparison.CurrentCultureIgnoreCase)
                    );
            }
            // Filter by MPAA Rating
            if (MPAARatings != null && MPAARatings.Length != 0)
            {
                Movies = Movies.Where(movie => 
                    movie.MPAARating != null && 
                    MPAARatings.Contains(movie.MPAARating)
                    );
            }
            if (Genres != null && Genres.Length != 0)
            {
                Movies = Movies.Where(movie =>
                    movie.MajorGenre != null &&
                    Genres.Contains(movie.MajorGenre)
                    );
            }
            if (IMDBMin != null || IMDBMax != null)
            {
                Movies = Movies.Where(movie =>
                    movie.IMDBRating != null && movie.IMDBRating >= IMDBMin || movie.IMDBRating <= IMDBMax
                    );
            }
            if (RottenTomatoesMin != null || RottenTomatoesMax != null)
            {
                Movies = Movies.Where(movie =>
                    movie.RottenTomatoesRating != null && movie.RottenTomatoesRating >= RottenTomatoesMin || movie.RottenTomatoesRating <= RottenTomatoesMax
                    );
            }
        }
    }
}
