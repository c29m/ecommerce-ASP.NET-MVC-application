using MovieTicket.Data.Base;
using MovieTicket.Models;

namespace MovieTicket.Data.Services
{
    public class DirectorsService : EntityBaseRepository<Director>, IDirectorsService
    {
        public DirectorsService(AppDbContext context) : base(context) { }
    }
}
