using System.ComponentModel.DataAnnotations;

namespace CheapMovies.Store
{
    public class Item
    {
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}