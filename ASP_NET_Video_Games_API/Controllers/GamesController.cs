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
                var totalSales = _context.VideoGames.Where(s => s.Platform == Platform).Where(s => s.Year > 2013).Select(s => s.GlobalSales).Sum();
                returnRes.Add(Platform, totalSales);
            }
            return Ok(returnRes);
        }
        [HttpGet("{gameName}")]
        public IActionResult GetGames(string gameName)
        {
            
            var gameInfo = _context.VideoGames.Where(g => g.Name.Contains(gameName));

            return Ok(gameInfo);
        }
        [HttpGet("allGames")]
        public IActionResult GetGames()
        {
            var videoGames = _context.VideoGames;
            return Ok(videoGames);
        }
        //^Get all

        [HttpGet("id/{id}")]
        public IActionResult GetGamesById(int id)
        {
            var videoGames = _context.VideoGames.Where(g => g.Id == id);
            return Ok(videoGames);
        }
        //^Get by Id

        [HttpGet("shootersByYear")]
        public IActionResult getShootersByYear()
        {
            var years = _context.VideoGames.Where(c => c.Year > 1980).Select(c => c.Year).Distinct();

            Dictionary<int, double> returnRes = new Dictionary<int, double>();
            foreach (int year in years.ToList())
            {

                var totalSales = _context.VideoGames.Where(s => s.Genre == "shooter").Where(s => s.Year == year).Select(s => s.GlobalSales).Sum();
                returnRes.Add(year, totalSales);
            }
            return Ok(returnRes);
        }

        [HttpGet("publishers")]
        public IActionResult getSalesByPublishers()
        {
            var consoles = _context.VideoGames.Select(c => c.Platform).Distinct();

            Dictionary<string, Dictionary<string, double>> returnRes = new Dictionary<string, Dictionary<string, double>>();
            foreach (string console in consoles.ToList())
            {
                Dictionary<string, double> pubSales = new Dictionary<string, double>();
                var pubs = _context.VideoGames.Where(p => p.Platform == console).OrderByDescending(p => p.GlobalSales).Select(p => p.Publisher).Distinct().Take(3);
                foreach (string pub in pubs.ToList())
                {
                    var globalSales = _context.VideoGames.Where(g => g.Publisher == pub).Select(g => g.GlobalSales).Sum();
                    pubSales.Add(pub, globalSales);
                }

                returnRes.Add(console, pubSales);
            }
            return Ok(returnRes);
        }



    }
}
