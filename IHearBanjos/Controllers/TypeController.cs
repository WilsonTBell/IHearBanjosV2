using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using IHearBanjos.Models;
using IHearBanjos.Repositories;

namespace IHearBanjos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeRepository _typeRepository;

        public TypeController(ITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_typeRepository.GetAllTypes());
        }
    }
}
