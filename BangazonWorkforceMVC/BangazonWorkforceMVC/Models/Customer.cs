using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AccountCreated { get; set; }

        public string LastActive { get; set; }

        public List<PaymentType> listOfPaymentTypes { get; set; } = new List<PaymentType>();

        public List<Product> listOfProducts { get; set; } = new List<Product>();

    }
}
