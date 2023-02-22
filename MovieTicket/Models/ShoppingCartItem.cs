using System.ComponentModel.DataAnnotations;

namespace MovieTicket.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }
        public Movie Movie { get; set; }
        
        public string ShoppingCartId {get; set; } 

    }
}
