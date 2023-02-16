using System.ComponentModel.DataAnnotations;

namespace MovieTicket.Models
{
    public class Actor
    {
        [Key]

        public int ActorId { get; set; }


        [Display(Name = "Profile Picture")]
        public string ProfilePictureURL { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Biography")]
        public string Bio { get; set; }


        /*-----Relationships-----*/

        public List<ActorMovie> ActorsMovies { get; set; }
    }
}
