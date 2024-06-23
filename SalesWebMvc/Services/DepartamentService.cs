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

        public List<Departament> GetAllDepartament()
        {
            return _context.Departament.OrderBy(x => x.Name).ToList();
        }

        public Departament GetDepartamentById(int id)
        {
            return _context.Departament.First(x => x.Id == id);
        }
    }
}
