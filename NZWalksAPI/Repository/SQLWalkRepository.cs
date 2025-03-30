using Microsoft.EntityFrameworkCore;
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

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var deleteWalk = await nZWalkDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (deleteWalk == null)
                return null;

            nZWalkDbContext.Walks.Remove(deleteWalk);
            nZWalkDbContext.SaveChanges();
            return deleteWalk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await nZWalkDbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await nZWalkDbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var updateWalk = await nZWalkDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (updateWalk == null)
            {
                return null;
            }

            updateWalk.Name = walk.Name;
            updateWalk.Description = walk.Description;
            updateWalk.LengthInKm = walk.LengthInKm;
            updateWalk.WalkImageUrl = walk.WalkImageUrl;
            updateWalk.RegionId = walk.RegionId;
            updateWalk.DifficultyId = walk.DifficultyId;

            await nZWalkDbContext.SaveChangesAsync();
            return updateWalk;

        }


    }
}
