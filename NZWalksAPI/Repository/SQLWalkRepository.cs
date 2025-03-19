using NZWalksAPI.Data;
using NZWalksAPI.Models.Domains;

namespace NZWalksAPI.Repository
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalkDbContext nZWalkDbContext;

        public SQLWalkRepository(NZWalkDbContext nZWalkDbContext)
        {
            this.nZWalkDbContext = nZWalkDbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await nZWalkDbContext.Walks.AddAsync(walk);
            await nZWalkDbContext.SaveChangesAsync();
            return walk;
        }
    }
}
