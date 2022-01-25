using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarGameAPI.Context;
using WarGameAPI.DTO;
using WarGameAPI.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WarGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PlayersController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("GetAllPlayers", Name = "GetAllPlayers")]
        public async Task<ActionResult<IEnumerable<Player>>> GetAllPlayers()
        {
            return await context.Players.ToListAsync();
        }

        [HttpGet("GetPlayerById/{id}", Name = "GetPlayerById")]
        public async Task<ActionResult<Player>> Get(int id)
        {
            Player player = await context.Players.FirstOrDefaultAsync(x => x.Id == id);

            if (player is null)
                return NotFound();

            return player;
        }

        [HttpPost("AddPlayer")]
        public async Task<ActionResult> Post([FromBody] Player player)
        {
            await context.Players.AddAsync(player);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("GetPlayerById",
                new { id = player.Id }, player);
        }

        [HttpPut("UpdatePlayers")]
        public async Task<ActionResult> Put([FromBody] PlayersResultsDTO player)
        {
            //Evaluate if both players exists in db
            Player playerToUpdate = await context.Players.FirstOrDefaultAsync(x => x.Id == player.Id);

            if (playerToUpdate is null)
                return NotFound("Player does not exist in db");

            if(player.WonGames > 0)
                playerToUpdate.WonGames = player.WonGames;
            if(player.LostGames > 0)
                playerToUpdate.LostGames = player.LostGames;

            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
