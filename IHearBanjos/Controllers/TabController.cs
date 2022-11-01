using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using IHearBanjos.Models;
using IHearBanjos.Repositories;
using Azure;


namespace IHearBanjos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabController : ControllerBase
    {
        private readonly ITabRepository _tabRepository;
        private readonly IBanjoistRepository _banjoistRepository;

        public TabController(ITabRepository tabRepository, IBanjoistRepository banjoistRepository)
        {
            _tabRepository = tabRepository;
            _banjoistRepository = banjoistRepository;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_tabRepository.GetAllTabs());
        }

        [HttpGet("MyTabs")]
        public IActionResult GetMyTabs()
        {
            Banjoist currentBanjoist = GetCurrentBanjoist();
            return Ok(_tabRepository.GetTabsByBanjoistId(currentBanjoist.Id));
        }

        [HttpGet("FavoriteTabs")]
        public IActionResult GetFavoriteTabs()
        {
            Banjoist currentBanjoist = GetCurrentBanjoist();
            return Ok(_tabRepository.GetBanjoistFavoriteTabs(currentBanjoist.Id));
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var tab = _tabRepository.GetTabById(id);
            if (tab == null)
            {
                return NotFound();
            }
            return Ok(tab);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post(Tab tab)
        {
            Banjoist currentBanjoist = GetCurrentBanjoist();
            tab.BanjoistId = currentBanjoist.Id;
            _tabRepository.Add(tab);
           
            return CreatedAtAction(nameof(Get), new { id = tab.Id }, tab);
        }

        [HttpPost("favorite")]
        public IActionResult Post(BanjoistFavorite banjoistFavorite)
        {
            Banjoist currentBanjoist = GetCurrentBanjoist();
            banjoistFavorite.BanjoistId = currentBanjoist.Id;
            _tabRepository.AddFavorite(banjoistFavorite);

            return CreatedAtAction(nameof(Get), new { id = banjoistFavorite.Id }, banjoistFavorite);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(Tab tab)
        {
            _tabRepository.UpdateTab(tab);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _tabRepository.DeleteTab(id);
        }

        [HttpDelete("favorite/{tabId}")]
        public void DeleteFavorite(int tabId)
        {
            var currentBanjoist = GetCurrentBanjoist();
            _tabRepository.DeleteFavorite(tabId, currentBanjoist.Id);
        }

        private Banjoist GetCurrentBanjoist()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _banjoistRepository.GetByFirebaseUserId(firebaseUserId);
        }
    }
}

