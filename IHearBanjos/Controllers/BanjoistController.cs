using IHearBanjos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using IHearBanjos.Models;
using IHearBanjos.Repositories;

namespace IHearBanjos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanjoistController : Controller
    {
            private readonly IBanjoistRepository _banjoistRepository;
            public BanjoistController(IBanjoistRepository banjoistRepository)
            {
                _banjoistRepository = banjoistRepository;
            }

            [HttpGet]
            public IActionResult Get()
            {
                return Ok(_banjoistRepository.GetAll());
            }

            [HttpGet("{firebaseUserId}")]
            public IActionResult GetBanjoist(string firebaseUserId)
            {
                return Ok(_banjoistRepository.GetByFirebaseUserId(firebaseUserId));
            }

            [HttpGet("DoesBanjoistExist/{firebaseUserId}")]
            public IActionResult DoesBanjoistExist(string firebaseUserId)
            {
                var banjoist = _banjoistRepository.GetByFirebaseUserId(firebaseUserId);
                if (banjoist == null)
                {
                    return NotFound();
                }
                return Ok();
            }

            [HttpGet("details/{id}")]
            public IActionResult GetBanjoistById(int id)
            {
                var banjoist = _banjoistRepository.GetById(id);
                if (banjoist == null)
                {
                    return NotFound();
                }
                return Ok(banjoist);
            }
            [HttpPost]
            public IActionResult Post(Banjoist banjoist)
            {
                banjoist.UserTypeId = UserType.AUTHOR_ID;
                _banjoistRepository.Add(banjoist);
                return CreatedAtAction(
                    nameof(GetBanjoist),
                    new { firebaseUserId = banjoist.FirebaseUserId },
                    banjoist);
            }
    }
}
