using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using SalesWebMvc.Models;
using SalesWebMvc.Models.enums;

namespace SalesWebMvc.Data
{
    public class SeedingService
    {
        private SalesWebMvcContext _context;
        public SeedingService(SalesWebMvcContext context)
        {
            _context = context;
        }
        public void Seed()
        {
            if (_context != null &&
                _context.Departament.Any() ||
                _context.Seller.Any() ||
                _context.SalesRecord.Any()
                )
            {
                return;
            }

            Departament d1 = new Departament("Eletronics", 1);
            Departament d2 = new Departament("Clothing", 2);
            Departament d3 = new Departament("Books", 3);
            Departament d4 = new Departament("Toys", 4);
            Departament d5 = new Departament("Home Appliances", 5);
            Departament d6 = new Departament("Sports", 6);
            Departament d7 = new Departament("Beauty", 7);


            Seller s1 = new Seller(1, "John Doe", "john@example.com", new DateTime(1990, 5, 15), 5000.00m, d1);
            Seller s2 = new Seller(2, "Jane Smith", "jane@example.com", new DateTime(1985, 8, 20), 5500.00m, d2);
            Seller s3 = new Seller(3, "Carlos Silva", "carlos@example.com", new DateTime(1988, 3, 10), 4800.00m, d3);
            Seller s4 = new Seller(4, "Maria Gonzalez", "maria@example.com", new DateTime(1992, 7, 5), 5200.00m, d4);
            Seller s5 = new Seller(5, "David Lee", "david@example.com", new DateTime(1995, 11, 25), 5100.00m, d5);
            Seller s6 = new Seller(6, "Laura Kim", "laura@example.com", new DateTime(1987, 12, 12), 4900.00m, d6);
            Seller s7 = new Seller(7, "Alexandre Santos", "alexandre@example.com", new DateTime(1993, 9, 8), 5300.00m, d7);


            SalesRecord sl1 = new SalesRecord(1, DateTime.Now, 1000.00m, SaleStatusEnum.BALLED, s1);
            SalesRecord sl2 = new SalesRecord(2, DateTime.Now.AddDays(-5), 750.00m, SaleStatusEnum.PENDING, s2);
            SalesRecord sl3 = new SalesRecord(3, DateTime.Now.AddDays(-10), 1200.00m, SaleStatusEnum.CANCELED, s3);
            SalesRecord sl4 = new SalesRecord(4, DateTime.Now.AddDays(-2), 900.00m, SaleStatusEnum.BALLED, s4);
            SalesRecord sl5 = new SalesRecord(5, DateTime.Now.AddDays(-7), 800.00m, SaleStatusEnum.PENDING, s5);
            SalesRecord sl6 = new SalesRecord(6, DateTime.Now.AddDays(-3), 1100.00m, SaleStatusEnum.BALLED, s6);
            SalesRecord sl7 = new SalesRecord(7, DateTime.Now.AddDays(-8), 950.00m, SaleStatusEnum.PENDING, s7);

            _context.Departament.AddRange(d1, d2, d3, d4, d5, d6, d7);
            _context.Seller.AddRange(s1, s2, s3, s4, s5, s6, s7);
            _context.SalesRecord.AddRange(sl1, sl2, sl3, sl4, sl5, sl6, sl7);

            _context.SaveChanges();

        }
    }
}
