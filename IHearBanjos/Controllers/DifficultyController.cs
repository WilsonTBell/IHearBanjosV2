using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using IHearBanjos.Models;
using IHearBanjos.Repositories;

namespace IHearBanjos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly IDifficultyRepository _difficultyRepository;

        public DifficultyController(IDifficultyRepository difficultyRepository)
        {
            _difficultyRepository = difficultyRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_difficultyRepository.GetAllDifficulties());
        }
    }
}