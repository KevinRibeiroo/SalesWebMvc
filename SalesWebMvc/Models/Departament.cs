namespace SalesWebMvc.Models
{
    public class Departament
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();
        public Departament() 
        {
        }

        public Departament(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public void AddSeller(Seller seller) 
        {
            Sellers.Add(seller);
        }
        public decimal CalcTotalSales(DateTime inicio, DateTime fim)
        {
            return Sellers.Sum(x => x.CalcTotalSales(inicio, fim));
        }
    }
}
