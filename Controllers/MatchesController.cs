using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarGameAPI.Context;
using WarGameAPI.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WarGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public MatchesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("StartNewGame", Name = "StartNewGame")]
        public List<string> StartNewGame()
        {
            List<string> deck = new List<string>() { "A♣", "K♣", "Q♣", "J♣", "10♣", "9♣", "8♣", "7♣", "6♣", "5♣", "4♣", "3♣", "2♣",
                                                     "A♦", "K♦", "Q♦", "J♦", "10♦", "9♦", "8♦", "7♦", "6♦", "5♦", "4♦", "3♦", "2♦",
                                                     "A♥", "K♥", "Q♥", "J♥", "10♥", "9♥", "8♥", "7♥", "6♥", "5♥", "4♥", "3♥", "2♥",
                                                     "A♠", "K♠", "Q♠", "J♠", "10♠", "9♠", "8♠", "7♠", "6♠", "5♠", "4♠", "3♠", "2♠"};

            Random rnd = new Random();
            var shuffledDeck = deck.OrderBy(item => rnd.Next()).ToList();
            shuffledDeck = shuffledDeck.OrderBy(item => rnd.Next()).ToList();
            shuffledDeck = shuffledDeck.OrderBy(item => rnd.Next()).ToList();

            return shuffledDeck;
        }

        // GET: api/values
        // List all matches
        [HttpGet("GetAllMatches", Name = "GetAllMatches")]
        public IEnumerable<Match> GetAllMatches()
        {
            return context.Matches.ToList();
        }

        // GET api/values/5
        // List the matches in which the player has participated
        [HttpGet("GetMatchesById/{id}", Name = "GetMatchesById")]
        public ActionResult<Match> GetMatchesById(int id)
        {
            var match = context.Matches.FirstOrDefault(x => x.Id == id);

            if (match is null)
                return NotFound();

            return match;
        }

        // GET api/values/5
        // List the matches in which the player has participated
        [HttpGet("GetMatchesByPlayerId/{id}", Name = "GetMatchesByPlayerId")]
        public ActionResult<List<Match>> GetMatchesByPlayerId(int id)
        {
            List<Match> match = context.Matches.Where(x => x.PlayerId1 == id || x.PlayerId2 == id).ToList();

            if (!match.Any())
                return NotFound();

            return match;
        }

        // POST api/values
        // Insert a new ended match
        [HttpPost("AddMatch")]
        public ActionResult Post([FromBody] Match match)
        {
            context.Matches.Add(match);
            context.SaveChanges();
            return new CreatedAtRouteResult("GetMatchesById",
                new { id = match.Id }, match);
        }
    }
}
