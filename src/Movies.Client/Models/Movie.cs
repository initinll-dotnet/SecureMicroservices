namespace Movies.Client.Models;

public record Movie
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Genre { get; init; }
    public string Rating { get; init; }
    public DateTime ReleaseDate { get; init; }
    public string ImageUrl { get; init; }
    public string Owner { get; init; }
}
