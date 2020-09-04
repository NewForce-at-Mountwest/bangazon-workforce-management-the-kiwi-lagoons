namespace BangazonWorkforceMVC.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int ProductTypeId { get; set; }

        public ProductType productType { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int CustomerId { get; set; }

        public Customer customer { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
