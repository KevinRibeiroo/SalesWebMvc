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

        public async Task<List<Seller>> GetAll()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task CreateSeller(Seller seller)
        {
            _context.Seller.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> GetSellerById(int id)
        {
            return await _context.Seller.Include(x => x.Departament).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveSeller(int id)
        {
            try
            {
                var seller = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException("Can't delete seller because he/she has sales");
            }
            
        }

        public async Task UpdateSeller(Seller seller)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == seller.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Vendedor não encontrado.");
            }
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
            
        }
    }
}
