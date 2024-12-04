using Entities.Entities.North;

namespace Repository.Contracts.Interfaces.North
{
    public interface IBranchNorthRepo
    {
        Task AddNewBranchAsync(BranchNorth branch);
        Task<IEnumerable<BranchNorth>> GetBranchListAsync();
        Task<BranchNorth> GetBranchAsync(Guid id);
        Task DeleteBranchAsync(BranchNorth branch);
        Task UpdateBranchAsync(BranchNorth branch);
    }
}
