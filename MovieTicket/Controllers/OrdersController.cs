using Microsoft.AspNetCore.Mvc;
using MovieTicket.Data.Cart;
using MovieTicket.Data.Services;
using MovieTicket.Data.ViewModels;

namespace MovieTicket.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;

        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _moviesService = moviesService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };


            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _moviesService.GetByIdAsync(id);

            if(item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if(item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();

            string userId = "";
            string userEmailAddress = "";
             
            await _ordersService.StoreOrdedrAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted"); 
        }

        public async Task<IActionResult> Index(){

            var userId = "";
            var orders = await _ordersService.GetAllOrdersByUserIdAsync(userId);

            return View(orders);
        }

    }
}
