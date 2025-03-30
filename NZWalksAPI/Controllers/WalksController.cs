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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walk = await walkRepository.GetAllAsync();

            return Ok(mapper.Map<List<WalkDto>>(walk));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walk = await walkRepository.GetByIdAsync(id);
            if (walk == null)
                return NotFound();

            return Ok(mapper.Map<WalkDto>(walk));
        }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            var walk = mapper.Map<Walk>(updateWalkRequestDto);

            walk = await walkRepository.UpdateAsync(id, walk);

            if (walk == null)
                return NotFound();

            return Ok(mapper.Map<WalkDto>(walk));
        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var requestDelete = await walkRepository.DeleteAsync(id);

            if (requestDelete == null)
                return NotFound();

            return Ok(mapper.Map<WalkDto>(requestDelete));
        }

    }

}
