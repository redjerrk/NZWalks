using NZWalksAPI.Models.Domains;

namespace NZWalksAPI.Repository
{
    public interface IWalkRepository
    {

        public Task<Walk> CreateAsync(Walk walk);

        public Task<List<Walk>> GetAllAsync();

        public Task<Walk?> GetByIdAsync(Guid id);

        public Task<Walk?> UpdateAsync(Guid id, Walk walk);

        public Task<Walk?> DeleteAsync(Guid id);
    }
}
