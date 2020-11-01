using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeTestDemo.Core.Entities;

namespace CodeTestDemo.Api.Controllers
{
    [Route("api/teams")]
    public class TeamController : ControllerBase
    {
        private static readonly string[] TeamNames = new[]
        {
           "Chelsea F.C.","Real Madrid","Krasnodar","Arsenal","FC Barcelona", "Man United","Ajax","Liverpool","Burnley","Man. City"
        };

        private readonly ILogger<TeamController> _logger;

        public TeamController(ILogger<TeamController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Team> Get()
        {
            return Enumerable.Range(0,TeamNames.Length).Select(index => new Team
            {
                TeamName = TeamNames[index]
            })
            .ToArray();
        }

    }
}
