using MovieTicket.Controllers;
using MovieTicket.Models;

namespace MovieTicket.Data.ViewModels
{
    public class NewMovieDropdownVM
    {
        

        public NewMovieDropdownVM()
        {
            Actors = new List<Actor>();
            Directors = new List<Director>();   
            Cinemas = new List<Cinema>();
            
        }

        public List<Director> Directors { get; set; }

        public List<Cinema> Cinemas { get; set; }

        public List<Actor> Actors { get; set; }
    }
}
