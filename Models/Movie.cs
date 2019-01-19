namespace Models
{
    using System;
    
    public class Movie
    {
        public string Name { get; set; }
        public DateTime ReleaseYear { get; set; }
        public string Country { get; set; }

        public override string  ToString() {
            return $"Name:{Name},ReleaseYear:{ReleaseYear.ToString()},Country:{Country}";
        }
    }
}