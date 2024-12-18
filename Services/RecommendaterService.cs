using DTOs.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;
using Service.Contracts;
using Services.MongoDB;

namespace Services
{
    public class RecommendaterService : IRecommendaterService
    {
        private readonly MongoDBClient _mongoDbClient;
        private readonly IMongoCollection<RecommendProduct> _recommendCollection;

        public RecommendaterService(MongoDBClient mongoDbClient) 
        {
            _mongoDbClient = mongoDbClient;
            _recommendCollection = _mongoDbClient.GetCollection<RecommendProduct>("recommendater");
        }

        public async Task CreateRecommendProductAsync(RecommendProduct recommendProduct)
        {
            await _recommendCollection.InsertOneAsync(recommendProduct);
        }

        public async Task<RecommendProduct> GetRecommendProductByProductIdAsync(string productId)
        {
            // Tạo bộ lọc tìm kiếm dựa trên ProductId
            var filter = Builders<RecommendProduct>.Filter.Eq(r => r.ProductId, productId);

            var projection = Builders<RecommendProduct>.Projection
                                .Exclude("_id"); // Loại bỏ _id

            var result = await _recommendCollection
                .Find(filter)
                .Project<RecommendProduct>(projection)
                .FirstOrDefaultAsync();

            // Trả về đối tượng hoặc null nếu không tìm thấy
            return result;
        }


        public async Task UpdateRecommendProductAsync(RecommendProduct recommendProduct)
        {
            var filter = Builders<RecommendProduct>.Filter.Eq(r => r.ProductId, recommendProduct.ProductId);
            
            var update = Builders<RecommendProduct>.Update
                .Set(r => r.ProductId, recommendProduct.ProductId)
                .Set(r => r.CategoryId, recommendProduct.CategoryId)
                .Set(r => r.Action, recommendProduct.Action)
                .Set(r => r.Purchase, recommendProduct.Purchase)
                ;

            await _recommendCollection.UpdateOneAsync(filter, update);
        }

        public async Task<List<RecommendProduct>> GetProductsByTotalActionAndPurchaseAsync()
        {
            // Tạo pipeline aggregation
            var pipeline = new[]
            {
                // Giai đoạn 3: Sắp xếp theo Total giảm dần
                new BsonDocument("$sort", new BsonDocument("$Action" + "$Purchase", -1)),
                // Giai đoạn 4: Loại bỏ _id
                new BsonDocument("$project", new BsonDocument
                    {
                        { "_id", 0 }, // Bỏ _id
                        { "ProductId", "$_id" }, // Đưa ProductId ra ngoài
                    })
            };

            // Thực hiện aggregation
            var result = await _recommendCollection.Aggregate<RecommendProduct>(pipeline).ToListAsync();

            return result;
        }

    }
}
