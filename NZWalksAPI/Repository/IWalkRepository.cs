using NZWalksAPI.Models.Domains;

namespace NZWalksAPI.Repository
{
    public interface IWalkRepository
    {

        public Task<Walk> CreateAsync(Walk walk);
    }
}
