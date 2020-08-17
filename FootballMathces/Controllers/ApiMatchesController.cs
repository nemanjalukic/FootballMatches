using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootballMathces.Models;
using FootballMatches.Models;

namespace FootballMatches.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiMatchesController : ControllerBase
    {
        private readonly FootballMatchesContext _context;

        public ApiMatchesController(FootballMatchesContext context)
        {
            _context = context;
        }

        // GET: api/ApiMatches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        {
            var matches= await _context.Matches.Include(m => m.Guest).Include(m => m.Host).Include(m=>m.Players).ThenInclude(pm=>pm.Player).ToListAsync();
            foreach(var item in matches)
            {
                List<PlayerInMatch> lists = item.Players.Where(p => p.Goals > 0).ToList();
                ICollection<PlayerInMatch> players = lists;
                ICollection<PlayerInMatch> hostPlayers= item.Players.Where(p => p.Player.TeamId==item.HostId && p.Player.Deleted==false).ToList();
                ICollection<PlayerInMatch> guestPlayers = item.Players.Where(p => p.Player.TeamId == item.GuestId && p.Player.Deleted == false).ToList();
                item.Players=players;
                item.HostPlayers = (List<PlayerInMatch>)hostPlayers;
                item.GuestPlayers = (List<PlayerInMatch>)guestPlayers;
               
            }
            return matches;
        }

        // GET: api/ApiMatches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);

            if (match == null)
            {
                return NotFound();
            }

            return match;
        }

        // PUT: api/ApiMatches/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutMatch(int id, Match match)
        {
            if (id != match.Id)
            {
                return BadRequest();
            }

            _context.Entry(match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ApiMatches
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Match>> PostMatch(Match match)
        {
            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatch", new { id = match.Id }, match);
        }

        // DELETE: api/ApiMatches/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Match>> DeleteMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return match;
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.Id == id);
        }*/
    }
}
