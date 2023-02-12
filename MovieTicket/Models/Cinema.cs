using System.ComponentModel.DataAnnotations;

namespace MovieTicket.Models
{
    public class Cinema
    {
        [Key]


        public int CinemaId { get; set; }


        public string Logo { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        /*-----Relationships-----*/

        public List<Movie> Movies { get; set; }
    }
}
