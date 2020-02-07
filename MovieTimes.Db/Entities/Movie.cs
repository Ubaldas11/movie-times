using System.Collections.Generic;

namespace MovieTimes.Db.Entities
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int DurationInMinutes { get; set; }
        public int Rating { get; set; }

        public List<MovieShowing> MovieShowings { get; set; }
    }
}
