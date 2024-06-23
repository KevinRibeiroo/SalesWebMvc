using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;

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
            return _context.Seller.Include(x => x.Departament).FirstOrDefault(x => x.Id == id);
        }

        public void RemoveSeller(int id)
        {
            var seller = _context.Seller.Find(id);
            _context.Seller.Remove(seller);
            _context.SaveChanges();
        }

        public void UpdateSeller(Seller seller)
        {
            if(!_context.Seller.Any(x => x.Id == seller.Id))
            {
                throw new NotFoundException("Vendedor não encontrado.");
            }
            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
            
        }
    }
}
