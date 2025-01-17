using System.Collections.Generic;
using System.Linq;
using WebMvc.Data;
using WebMvc.Models;

namespace WebMvc.Services
{
    public class SellerService
    {
        private readonly WebMvcContext _context;

        public SellerService(WebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }
    }
}
