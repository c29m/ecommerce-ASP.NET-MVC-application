using System.ComponentModel.DataAnnotations;

namespace MovieTicket.Models
{
    public class Director
    {
        [Key]

        public int DirectorId { get; set; }

        [Display(Name = "Profile Picture")]
        public string ProfilePictureURL { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Biography")]
        public string Bio { get; set; }


        /*-----Relationships-----*/

        public List<Movie> Movies { get; set; }
    }
}
