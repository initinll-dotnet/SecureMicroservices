using Movies.Client.Models;

using Refit;

namespace Movies.Client.Services;

public interface IMovieApiService
{
    [Get("/api/Movies")]
    Task<IEnumerable<Movie>> GetMovies();

    [Get("/api/Movies/{id}")]
    Task<Movie> GetMovie(int id);

    [Post("/api/Movies")]
    Task<Movie> CreateMovie([Body] Movie movie);

    [Put("/api/Movies/{id}")]
    Task<Movie> UpdateMovie(int id, [Body] Movie movie);

    [Delete("/api/Movies/{id}")]
    Task DeleteMovie(int id);
}
