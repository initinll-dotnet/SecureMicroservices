namespace Movies.API.Data;

public static class SeedDBExtension
{
    public static WebApplication SeedDB(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<MoviesAPIContext>();

            MoviesContextSeed.SeedAsync(dbContext);
        }

        return app;
    }
}
