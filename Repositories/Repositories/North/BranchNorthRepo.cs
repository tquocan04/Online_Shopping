using Entities.Context;
using Entities.Entities.North;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces.North;

namespace Repositories.Repositories.North
{
    public class BranchNorthRepo : IBranchNorthRepo
    {
        private readonly ApplicationContextNorth _applicationContextNorth;

        public BranchNorthRepo(ApplicationContextNorth applicationContextNorth) 
        {
            _applicationContextNorth = applicationContextNorth;
        }
        public async Task AddNewBranchAsync(BranchNorth branch)
        {
            _applicationContextNorth.Branches.Add(branch);
            await _applicationContextNorth.SaveChangesAsync();

        }

        public async Task DeleteBranchAsync(BranchNorth branch)
        {
            _applicationContextNorth.Branches.Remove(branch);
            await _applicationContextNorth.SaveChangesAsync();
        }

        public Task<BranchNorth> GetBranchAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BranchNorth>> GetBranchListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateBranchAsync(BranchNorth branch)
        {
            _applicationContextNorth.Branches.Update(branch);
            await _applicationContextNorth.SaveChangesAsync();
        }
    }
}
