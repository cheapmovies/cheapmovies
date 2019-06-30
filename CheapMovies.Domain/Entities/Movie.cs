using System;
using Newtonsoft.Json.Linq;

namespace CheapMovies.Domain.Entities
{
    public class Movie
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
        public string Poster { get; set; }
        public string Metascore { get; set; }
        public string Rating { get; set; }
        public string Votes { get; set; }
        public string FullId { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public string Price { get; set; }
        public bool FromStore { get; set; }

        public Movie(string jsonString): this(JObject.Parse(jsonString))
        {
        }
        
        public Movie(JObject json)
        {
            this.Title = (string)json["Title"];
            this.Year = (string)json["Year"];
            this.Rated = (string)json["Rated"];
            this.Released = (string)json["Released"];
            this.Runtime = (string)json["Runtime"];
            this.Genre = (string)json["Genre"];
            this.Director = (string)json["Director"];
            this.Writer = (string)json["Writer"];
            this.Actors = (string)json["Actors"];
            this.Plot = (string)json["Plot"];
            this.Language = (string)json["Language"];
            this.Country = (string)json["Country"];
            this.Awards = (string)json["Awards"];
            this.Poster = (string)json["Poster"];
            this.Metascore = (string)json["Metascore"];
            this.Rating = (string)json["Rating"];
            this.Votes = (string)json["Votes"];
            this.FullId = (string)json["ID"];
            this.Id = this.FullId.Substring(2);
            this.Type = (string)json["Type"];
            this.Price = (string)json["Price"];
            this.FromStore = false;
        }
    }
}
