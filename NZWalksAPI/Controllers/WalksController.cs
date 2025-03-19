using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domains;
using NZWalksAPI.Models.Dtos;
using NZWalksAPI.Repository;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }



        //creating a new walk

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walk = mapper.Map<Walk>(addWalkRequestDto);

            await walkRepository.CreateAsync(walk);

            return Ok(mapper.Map<WalkDto>(walk));

        }

    }
}
