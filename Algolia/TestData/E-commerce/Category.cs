namespace Algolia.TestData
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int objectID
        {
            get { return CategoryId; }
        }
    }
}