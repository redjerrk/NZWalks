using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domains;
using NZWalksAPI.Models.Dtos;
using NZWalksAPI.Repository;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalkDbContext _db;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalkDbContext db, IRegionRepository regionRepository, IMapper mapper)
        {
            _db = db;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        // GET: api/Regions
        // GET: https://localhost:5001/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var regions = await regionRepository.GetAllAsync();

            //var regionDto = new List<RegionDto>();

            //foreach (var region in regions)
            //{
            //    regionDto.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl

            //    });


            //}

            var regionDto = mapper.Map<List<RegionDto>>(regions);

            return Ok(regionDto);
        }

        // GET: api/Regions/{id}
        // GET: https://localhost:5001/api/regions/{id}

        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> Get(Guid id)
        {

            //var regions = _db.Regions.Find(id);

            var region = await regionRepository.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            //var regionDto = new RegionDto
            //{
            //    Id = region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl
            //};

            var regionDto = mapper.Map<RegionDto>(region);
            return Ok(regionDto);

        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //var region = new Region
            //{
            //    Code = addRegionRequestDto.Code,
            //    Name = addRegionRequestDto.Name,
            //    RegionImageUrl = addRegionRequestDto.RegionImageUrl
            //};

            var region = mapper.Map<Region>(addRegionRequestDto);

            region = await regionRepository.CreateAsync(region);

            //var regionDto = new RegionDto
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            var regionDto = mapper.Map<RegionDto>(region);
            return CreatedAtAction(nameof(Get), new { id = regionDto.Id }, regionDto);
        }

        //Update
        //PUT
        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateRegionRequestDto updateRegionRequestDto)
        {
            //var region = new Region
            //{
            //    Code = updateRegionRequestDto.Code,
            //    Name = updateRegionRequestDto.Name,
            //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            //};

            var region = mapper.Map<Region>(updateRegionRequestDto);

            region = await regionRepository.UpdateAsync(id, region);

            if (region == null)
            {
                return NotFound();
            }

            //var regionDto = new RegionDto
            //{
            //    Id = region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            var regionDto = mapper.Map<RegionDto>(region);
            return Ok(regionDto);

        }

        //Delete

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var region = await regionRepository.DeleteAsync(id);

            if (region == null)
            {
                return NotFound();
            }


            //var regionDto = new RegionDto
            //{
            //    Id = region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            var regionDto = mapper.Map<RegionDto>(region);

            return Ok(regionDto);
        }


    }
}
