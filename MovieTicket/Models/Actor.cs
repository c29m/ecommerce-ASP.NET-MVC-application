using System.ComponentModel.DataAnnotations;

namespace MovieTicket.Models
{
    public class Actor
    {
        [Key]

        public int ActorId { get; set; }


        public string ProfilePictureURL { get; set; }

        public string FullName { get; set; }

        public string Bio { get; set; }


        /*-----Relationships-----*/

        public List<ActorMovie> ActorsMovies { get; set; }
    }
}
