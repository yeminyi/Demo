using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeTestDemo.Core.Entities;

namespace CodeTestDemo.Api.Controllers
{
    [Route("api/games")]
    public class GameController :ControllerBase
    {
        private static readonly string[] GameNames = new[]
        {
            "UEFA Champions League Final", "UEFA Champions League 1/2", "UEFA Champions League 1/4", "UEFA Champions League 1/8", "UEFA Champions League 1/16",  "UEFA Champions League Group" ,"UEFA Champions League Group A","UEFA Champions League Group B","UEFA Champions League Group C","UEFA Champions League Group D"
        };

        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Game> Get()
        {
            return Enumerable.Range(0, GameNames.Length).Select(index => new Game
            {
                GameName = GameNames[index]
            })
            .ToArray();
        }
    }
}
