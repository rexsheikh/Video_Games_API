using ASP_NET_Video_Games_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_Video_Games_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetConsoles()
        {
            var videoGameConsoles = _context.VideoGames.Select(vg => vg.Platform).Distinct();

            return Ok(videoGameConsoles);
        }

        [HttpGet("{platName}")]
        public IActionResult GetSalesByPlatform(string platName)
        {
            var totalSales = _context.VideoGames.Where(s => s.Platform == platName).Select(s => s.GlobalSales).Sum();
            return Ok(totalSales);
        }
        [HttpGet("names")]
        public IActionResult GetGameNames()
        {
            var videoGameNames = _context.VideoGames.Select(vg => vg.Name).Distinct();

            return Ok(videoGameNames);
        }

        [HttpGet("gameName/{gameName}")]
        public IActionResult GetGameInfo(string gameName)
        {
            var gameInfo = _context.VideoGames.Where(g => g.Name == gameName);

            return Ok(gameInfo);
        }

    }
}
