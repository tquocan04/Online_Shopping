namespace DTOs.MongoDb
{
    public class ProductMetadata
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Image { get; set; }
        public string? CategoryName { get; set; }
    }
}
