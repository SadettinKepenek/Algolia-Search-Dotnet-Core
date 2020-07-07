using System;

namespace Algolia.TestData
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public int objectID
        {
            get { return CustomerId; }
        }
    }
}