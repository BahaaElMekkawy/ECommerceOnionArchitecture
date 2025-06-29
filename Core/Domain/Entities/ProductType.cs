
namespace Domain.Entities
{
    public class ProductType :BaseEntity<int>
    {
        public string Name { get; set; }

        //public ICollection<Product> Products { get; set; } //We don't need it because i will not access the product through the Type or Brand
    }
}
