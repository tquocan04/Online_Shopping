using DTOs.MongoDb;

namespace Service.Contracts
{
    public interface IRecommendaterService
    {
        Task CreateRecommendProductAsync(RecommendProduct recommendProduct);
        Task<RecommendProduct> GetRecommendProductByProductIdAsync(string productId);
        Task UpdateRecommendProductAsync(RecommendProduct recommendProduct);
    }
}
