namespace BangazonWorkforceMVC.Models
{
    public class PaymentType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AccountNumber { get; set; }

        public int CustomerId { get; set; }

        public Customer customer { get; set; }
    }
}
