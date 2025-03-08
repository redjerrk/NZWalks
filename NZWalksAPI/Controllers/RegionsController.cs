using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domains;
using NZWalksAPI.Models.Dtos;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalkDbContext _db;
        public RegionsController(NZWalkDbContext db)
        {
            _db = db;
        }


        // GET: api/Regions
        // GET: https://localhost:5001/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = _db.Regions.ToList();
            var regionDto = new List<RegionDto>();

            foreach (var region in regions)
            {
                regionDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl

                });


            }

            return Ok(regionDto);
        }

        // GET: api/Regions/{id}
        // GET: https://localhost:5001/api/regions/{id}

        [HttpGet]
        [Route("{id}")]

        public IActionResult Get(Guid id)
        {

            //var regions = _db.Regions.Find(id);

            var region = _db.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);

        }


        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var region = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            _db.Regions.Add(region);
            _db.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return CreatedAtAction(nameof(Get), new { id = regionDto.Id }, regionDto);
        }

        //Update
        //PUT
        [HttpPut]
        [Route("{id:Guid}")]

        public IActionResult Update([FromRoute] Guid id, UpdateRegionRequestDto updateRegionRequestDto)
        {
            var region = _db.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            region.Name = updateRegionRequestDto.Name;
            region.Code = updateRegionRequestDto.Code;
            region.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            _db.SaveChanges();


            var returnDto = new RegionDto
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };
            return Ok(returnDto);

        }

        //Delete

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var region = _db.Regions.FirstOrDefault(x => x.Id == id);
            if (region == null)
            {
                return NotFound();
            }
            _db.Regions.Remove(region);
            _db.SaveChanges();
            return NoContent();
        }


    }
}
