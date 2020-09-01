using System;
using System.Collections.Generic;

namespace BangazonAPI.Models
{
    public class Order
    {
        public int Id { get; set; }

        public Nullable<int> PaymentTypeId { get; set; }

        public PaymentType paymentType { get; set; }

        public int CustomerId { get; set; }

        public Customer customer { get; set; }

        public List<Product> listOfProducts { get; set; } = new List<Product>();

    }
}
