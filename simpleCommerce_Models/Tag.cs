namespace simpleCommerce_Models
{
    public class Tag
    {
        public Guid TagId { get; set; }
        public string Name { get; set; } = default!;
        public string FilterName { get; set; } = default!;
    }
}
