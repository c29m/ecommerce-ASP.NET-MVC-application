using System.ComponentModel.DataAnnotations;

namespace MovieTicket.Models
{
    public class Director
    {
        [Key]

        public int DirectorId { get; set; }


        public string ProfilePictureURL { get; set; }

        public string FullName { get; set; }

        public string Bio { get; set; }


        /*-----Relationships-----*/

        public List<Movie> Movies { get; set; }
    }
}
