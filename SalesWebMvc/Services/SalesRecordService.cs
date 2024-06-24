using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> GetSalesByDateAsync(DateTime? initial,  DateTime? endDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (initial.HasValue)
            {
                result = result.Where(x => x.Date >= initial);
            }
            if (endDate.HasValue)
            {
                result = result.Where(x => x.Date <= endDate);
            }

            return await result.Include(x => x.Seller)
                .Include(x => x.Seller.Departament)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Departament,SalesRecord>>> GetSalesByDateGroupingAsync(DateTime? initial, DateTime? endDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (initial.HasValue)
            {
                result = result.Where(x => x.Date >= initial);
            }
            if (endDate.HasValue)
            {
                result = result.Where(x => x.Date <= endDate);
            }

            return await result.Include(x => x.Seller)
                .Include(x => x.Seller.Departament)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Departament)
                .ToListAsync();
        }
    }
}
