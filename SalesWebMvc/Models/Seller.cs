using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} Required")]
        [StringLength(60, MinimumLength = 6, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal Salario { get; set; }

        [ValidateNever]
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
