using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> GetAll()
        {
            return _context.Seller.ToList();
        }

        public void CreateSeller(Seller seller)
        {
            _context.Seller.Add(seller);
            _context.SaveChanges();
        }

        public Seller GetSellerById(int id)
        {
            return _context.Seller.FirstOrDefault(x => x.Id == id);
        }

        public void RemoveSeller(int id)
        {
            var seller = _context.Seller.Find(id);
            _context.Seller.Remove(seller);
            _context.SaveChanges();
        }
    }
}
