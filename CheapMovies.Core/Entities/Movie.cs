using System;

namespace CheapMovies.Core
{
    public class Movie
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; } // denormalise
        public string Director { get; set; }
        public string Actors { get; set; } // denormalise
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Poster { get; set; }
        public int Metascore { get; set; }
        public double Rating { get; set; }
        public string Votes { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
    }
}
