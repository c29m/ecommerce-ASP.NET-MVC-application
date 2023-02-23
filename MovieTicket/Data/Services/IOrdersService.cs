using MovieTicket.Models;

namespace MovieTicket.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrdedrAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);

        Task<List<Order>> GetAllOrdersByUserIdAsync(string userId);
        
    }
}
