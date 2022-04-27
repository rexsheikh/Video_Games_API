using ASP_NET_Video_Games_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public IActionResult getSalesByConsole()
        {
            var consoles = _context.VideoGames.Select(c => c.Platform).Distinct();

            Dictionary<string, double> returnRes = new Dictionary<string, double>();
            foreach (string Platform in consoles.ToList())
            {
                var totalSales = _context.VideoGames.Where(s => s.Platform == Platform).Select(s => s.GlobalSales).Sum();
                returnRes.Add(Platform, totalSales);
            }
            return Ok(returnRes);
        }
    }
}
