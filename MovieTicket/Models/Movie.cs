using System.ComponentModel.DataAnnotations;
using MovieTicket.Data;

namespace MovieTicket.Models
{
    public class Movie
    {
        [Key]

        public int MovieId { get; set; }


        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string ImageURL { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Genre Genre { get; set; }


    }
}
