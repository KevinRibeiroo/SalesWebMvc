using System.Reflection.Metadata.Ecma335;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal Salario { get; set; }
        public Departament Departament { get; set; }
        public int DepartamentId { get; set; }
        public ICollection<SalesRecord> SalesRecords { get; set; } = new List<SalesRecord>();

        public Seller() 
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, decimal salario, Departament departament)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Salario = salario;
            Departament = departament;
        }

        public void AddSalesRecord(SalesRecord record) 
        {
            SalesRecords.Add(record);
        }
        public void RemoveSalesRecord(SalesRecord record) 
        {
            SalesRecords.Remove(record);
        }
        public decimal CalcTotalSales(DateTime inicio, DateTime fim)
        {
            return SalesRecords.Where(x => x.Date >= inicio && x.Date <= fim).Sum(x => x.Amount);
        }
    }
}
