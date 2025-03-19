using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domains;

namespace NZWalksAPI.Repository
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalkDbContext nZWalkDbContext;

        public SQLRegionRepository(NZWalkDbContext nZWalkDbContext)
        {
            this.nZWalkDbContext = nZWalkDbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await nZWalkDbContext.Regions.AddAsync(region);
            await nZWalkDbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var requestDelete = await nZWalkDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (requestDelete == null)
            {
                return null;
            }

            nZWalkDbContext.Regions.Remove(requestDelete);
            await nZWalkDbContext.SaveChangesAsync();
            return requestDelete;

        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await nZWalkDbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await nZWalkDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var requestUpdate = await nZWalkDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (requestUpdate == null)
            {
                return null;
            }
            requestUpdate.Code = region.Code;
            requestUpdate.Name = region.Name;
            requestUpdate.RegionImageUrl = region.RegionImageUrl;


            await nZWalkDbContext.SaveChangesAsync();

            return region;

        }
    }
}
