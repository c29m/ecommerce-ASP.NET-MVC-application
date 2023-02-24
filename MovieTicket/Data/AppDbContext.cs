using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MovieTicket.Models;

namespace MovieTicket.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorMovie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });

            modelBuilder.Entity<ActorMovie>().HasOne(m => m.Movie).WithMany(am => am.MovieActors).HasForeignKey(m=> 
                m.MovieId);
            modelBuilder.Entity<ActorMovie>().HasOne(m => m.Actor).WithMany(am => am.ActorsMovies).HasForeignKey(m => 
                m.ActorId);

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Actor> Actors  { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<ActorMovie> ActorsMovies { get; set; }

        //Order related

       public DbSet<Order> Orders { get; set; }

       public DbSet<OrderItem> OrderItems { get; set; }

       public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
