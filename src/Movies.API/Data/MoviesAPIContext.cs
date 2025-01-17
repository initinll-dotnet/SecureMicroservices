﻿using Microsoft.EntityFrameworkCore;

namespace Movies.API.Data
{
    public class MoviesAPIContext : DbContext
    {
        public MoviesAPIContext(DbContextOptions<MoviesAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Movies.API.Models.Movie> Movie { get; set; } = default!;
    }
}
