using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class DepartamentService
    {
        private readonly SalesWebMvcContext _context;
        public DepartamentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public void CreateDepartament(Departament departament)
        {
            _context.Departament.Add(departament);
            _context.SaveChanges();
        }

        public async Task<List<Departament>> GetAllDepartament()
        {
            return await _context.Departament.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Departament> GetDepartamentById(int id)
        {
            return await _context.Departament.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
