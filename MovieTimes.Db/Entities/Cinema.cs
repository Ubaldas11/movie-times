using MovieTimes.Db.Enums;

namespace MovieTimes.Db.Entities
{
    public class Cinema
    {
        public int CinemaId { get; set; }
        public string CinemaName { get; set; }
        public City City { get; set; }
    }
}
