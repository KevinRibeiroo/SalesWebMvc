namespace SalesWebMvc.Models.viewModels
{
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }
        public ICollection<Departament> Departaments { get; set;}
    }
}
