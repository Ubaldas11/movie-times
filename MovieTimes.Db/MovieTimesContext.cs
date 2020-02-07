using Microsoft.EntityFrameworkCore;
using MovieTimes.Db.Entities;

namespace MovieTimes.Db
{
    public class MovieTimesContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieTheater> MovieTheaters { get; set; }
        public DbSet<MovieShowing> MovieShowings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => 
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MovieTimes;Integrated Security=True");
    }
}
