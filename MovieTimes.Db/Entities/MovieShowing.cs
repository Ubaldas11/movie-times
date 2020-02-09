using System;

namespace MovieTimes.Db.Entities
{
    public class MovieShowing
    {
        public int MovieShowingId { get; set; }
        public DateTime StartTime { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }
    }
}
