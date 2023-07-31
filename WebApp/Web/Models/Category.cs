namespace Web.Models
{
    public class Category
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
