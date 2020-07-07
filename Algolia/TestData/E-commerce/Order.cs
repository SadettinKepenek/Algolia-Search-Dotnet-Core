using System;

namespace Algolia.TestData
{
    public class Order
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public int objectID
        {
            get { return OrderId; }
        }

        public Product Product { get; set; }
        public Customer Customer { get; set; }

    }
}