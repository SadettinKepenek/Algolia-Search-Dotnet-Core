using System;

namespace Algolia.TestData
{
    public class Product
    {
        public int ProductId{ get; set; }

        public int objectID
        {
            get { return ProductId; }
        }
        public string Name { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}