﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieTicket.Data.Enums;

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


        /*-----Relationships-----*/

        public List<ActorMovie> MovieActors { get; set; }

        //Cinema

        public int CinemaId { get; set; }
        [ForeignKey("CinemaIb")]

        public Cinema Cinema { get; set; }

        //Director

        public int DirectorId { get; set; }
        [ForeignKey("DirectorId")]

        public Director Director { get; set; }


    }
}
