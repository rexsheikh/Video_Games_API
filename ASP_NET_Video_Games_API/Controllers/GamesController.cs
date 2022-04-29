﻿using ASP_NET_Video_Games_API.Data;
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

            Dictionary<double, string> returnRes = new Dictionary<double, string>();
            foreach (int year in years.ToList())
            {

                var totalSales = _context.VideoGames.Where(s => s.Genre == "shooter").Where(s => s.Year == year).Select(s => s.GlobalSales).Sum().ToString();
                returnRes.Add(year, totalSales);
            }
            return Ok(returnRes);
        }

    }
}
