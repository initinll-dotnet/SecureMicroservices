using Movies.API.Models;

namespace Movies.API.Data;

public class MoviesContextSeed
{
    public static void SeedAsync(MoviesAPIContext moviesAPIContext)
    {
        if (!moviesAPIContext.Movie.Any())
        {
            var movies = new List<Movie>
                {
                    new Movie
                    {
                        Id = 1,
                        Genre = "Drama",
                        Title = "The Shawshank Redemption",
                        Rating = "9.3",
                        ImageUrl = "https://m.media-amazon.com/images/I/51WYsbIa7TS._AC_UF1000,1000_QL80_.jpg",
                        ReleaseDate = new DateTime(1994, 5, 5),
                        Owner = "alice"
                    },
                    new Movie
                    {
                        Id = 2,
                        Genre = "Crime",
                        Title = "The Godfather",
                        Rating = "9.2",
                        ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTXVyjle_FY82uDeODrCjp4moI3RRXcoAMWOFfcpIpnrh9frNq_577ebXr2n2u5UeNdz2A&usqp=CAU",
                        ReleaseDate = new DateTime(1972, 5, 5),
                        Owner = "alice"
                    },
                    new Movie
                    {
                        Id = 3,
                        Genre = "Action",
                        Title = "The Dark Knight",
                        Rating = "9.1",
                        ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQmy2_q5DtAm4wWFXmk5I-8iaSLd-IjtEilBa5sZTh6a0kkCipojvcY-gj2cplCy923sHs&usqp=CAU",
                        ReleaseDate = new DateTime(2008, 5, 5),
                        Owner = "bob"
                    },
                    new Movie
                    {
                        Id = 4,
                        Genre = "Crime",
                        Title = "12 Angry Men",
                        Rating = "8.9",
                        ImageUrl = "https://rukminim2.flixcart.com/image/850/1000/kmmcrrk0/poster/w/o/i/medium-12-angry-man-wall-decor-paper-12-18-rolled-print-poster-original-imagfha3u4rj4y2k.jpeg?q=90&crop=false",
                        ReleaseDate = new DateTime(1957, 5, 5),
                        Owner = "bob"
                    },
                    new Movie
                    {
                        Id = 5,
                        Genre = "Biography",
                        Title = "Schindler's List",
                        Rating = "8.9",
                        ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRp7G_8aX8UVkfLtuub9zWHN7oLZCLlN6OrpyDnu0VbJMgdPNjz-iTgTVkIIfOgmpvzr4s&usqp=CAU",
                        ReleaseDate = new DateTime(1993, 5, 5),
                        Owner = "alice"
                    },
                    new Movie
                    {
                        Id = 6,
                        Genre = "Drama",
                        Title = "Pulp Fiction",
                        Rating = "8.9",
                        ImageUrl = "https://m.media-amazon.com/images/I/91peDnalJQL.jpg",
                        ReleaseDate = new DateTime(1994, 5, 5),
                        Owner = "alice"
                    },
                    new Movie
                    {
                        Id = 7,
                        Genre = "Drama",
                        Title = "Fight Club",
                        Rating = "8.8",
                        ImageUrl = "https://mir-s3-cdn-cf.behance.net/project_modules/hd/d6aeda57161475.59cb08fe94e68.jpg",
                        ReleaseDate = new DateTime(1999, 5, 5),
                        Owner = "bob"
                    },
                    new Movie
                    {
                        Id = 8,
                        Genre = "Romance",
                        Title = "Forrest Gump",
                        Rating = "8.8",
                        ImageUrl = "https://divinepicturesoftruth.in/cdn/shop/files/Forrest_gump.jpg?v=1720779177",
                        ReleaseDate = new DateTime(1994, 5, 5),
                        Owner = "bob"
                    }
                };

            moviesAPIContext.Movie.AddRange(movies);
            moviesAPIContext.SaveChanges();
        }
    }
}
