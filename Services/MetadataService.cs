using DTOs.MongoDb;
using MongoDB.Driver;
using Service.Contracts;
using Services.MongoDB;

namespace Services
{
    public class MetadataService : IMetadataService
    {
        private readonly MongoDBClient _mongoDbClient;
        private readonly IMongoCollection<CustomerMetadata> _customerCollection;
        private readonly IMongoCollection<ProductMetadata> _productMetadata;

        public MetadataService(MongoDBClient mongoDbClient)
        {
            _mongoDbClient = mongoDbClient;
            _customerCollection = _mongoDbClient.GetCollection<CustomerMetadata>("customer");
            _productMetadata = _mongoDbClient.GetCollection<ProductMetadata>("product");
        }
        public async Task CreateCustomerMetadataAsync(CustomerMetadata metadata)
        {
            await _customerCollection.InsertOneAsync(metadata);
        }

        public async Task CreateProductMetadataAsync(ProductMetadata metadata)
        {
            await _productMetadata.InsertOneAsync(metadata);
        }

        public async Task UpdateCustomerMetadataAsync(CustomerMetadata metadata)
        {
            var filter = Builders<CustomerMetadata>.Filter.Eq(c => c.Id, metadata.Id);
            var update = Builders<CustomerMetadata>.Update
                .Set(c => c.Name, metadata.Name)
                .Set(c => c.Email, metadata.Email)
                .Set(c => c.Password, metadata.Password)
                .Set(c => c.PhoneNumber, metadata.PhoneNumber)
                .Set(c => c.Picture, metadata.Picture)
                .Set(c => c.Dob, metadata.Dob)
                .Set(c => c.Gender, metadata.Gender)
                .Set(c => c.RegionName, metadata.RegionName)
                .Set(c => c.CityName, metadata.CityName)
                .Set(c => c.DistrictName, metadata.DistrictName)
                .Set(c => c.Street, metadata.Street)
                ;

            await _customerCollection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateProductMetadataAsync(ProductMetadata metadata)
        {
            var filter = Builders<ProductMetadata>.Filter.Eq(p => p.Id, metadata.Id);
            var update = Builders<ProductMetadata>.Update
                .Set(p => p.Name, metadata.Name)
                .Set(p => p.Description, metadata.Description)
                .Set(p => p.Price, metadata.Price)
                .Set(p => p.Image, metadata.Image)
                .Set(p => p.CategoryName, metadata.CategoryName)
                ;

            await _productMetadata.UpdateOneAsync(filter, update);
        }

        public async Task DeleteProductMetadataAsync(ProductMetadata metadata)
        {
            var filter = Builders<ProductMetadata>.Filter.Eq(p => p.Id, metadata.Id);
            await _productMetadata.DeleteOneAsync(filter);
        }
    }
}
