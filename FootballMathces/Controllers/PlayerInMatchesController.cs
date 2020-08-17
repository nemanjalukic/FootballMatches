using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FootballMatches.Models;
using FootballMathces.Models;

namespace FootballMatches.Controllers
{
    public class PlayerInMatchesController : Controller
    {
        private readonly FootballMatchesContext _context;

        public PlayerInMatchesController(FootballMatchesContext context)
        {
            _context = context;
        }

        // GET: PlayerInMatches
        public async Task<IActionResult> Index()
        {
            var footballMatchesContext = _context.PlayerInMatch.Include(p => p.Match).Include(p => p.Player);
            return View(await footballMatchesContext.ToListAsync());
        }

        // GET: PlayerInMatches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerInMatch = await _context.PlayerInMatch
                .Include(p => p.Match)
                .Include(p => p.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerInMatch == null)
            {
                return NotFound();
            }

            return View(playerInMatch);
        }

        // GET: PlayerInMatches/Create
        public IActionResult Create()
        {
            ViewData["MatchId"] = new SelectList(_context.Matches, "Id", "Id");
            ViewData["PlayerId"] = new SelectList(_context.Player, "Id", "Name");
            return View();
        }

        // POST: PlayerInMatches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlayerId,Goals,IsChecked,MatchId")] PlayerInMatch playerInMatch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playerInMatch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MatchId"] = new SelectList(_context.Matches, "Id", "Id", playerInMatch.MatchId);
            ViewData["PlayerId"] = new SelectList(_context.Player, "Id", "Name", playerInMatch.PlayerId);
            return View(playerInMatch);
        }

        // GET: PlayerInMatches/Edit/5
        public async Task<IActionResult> Edit(string side, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches.Include(m=>m.Host).Include(m=>m.Guest).Include(m=>m.Players).ThenInclude(pm=>pm.Player).FirstOrDefaultAsync(m => m.Id ==id);
            if (side.Equals("host"))
            {
                ViewData["PlayerId"] = new SelectList(match.Players.Select(pm => pm.Player).Where(p => p.TeamId == match.HostId).ToList(), "Id", "Name");
                ViewData["Name"] = match.Host.Name;
            }
            else
            {
                ViewData["PlayerId"] = new SelectList(match.Players.Select(pm => pm.Player).Where(p => p.TeamId == match.GuestId).ToList(), "Id", "Name");
                ViewData["Name"] = match.Guest.Name;
            }

            return View(new PlayerInMatch { MatchId= (int)id ,Match=match});
        }

        // POST: PlayerInMatches/Edit?side=host&id=5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("PlayerId,MatchId")] PlayerInMatch playerInMatch)
        {
            var pm = await _context.PlayerInMatch.Include(p => p.Player).Include(p=>p.Match).FirstOrDefaultAsync(p => p.MatchId == playerInMatch.MatchId && p.PlayerId == playerInMatch.PlayerId);
            playerInMatch = pm;
            if (ModelState.IsValid)
            {
                try
                {
                    playerInMatch.AddGoal();
                    _context.Update(playerInMatch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerInMatchExists(playerInMatch.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            //ViewData["MatchId"] = new SelectList(_context.Matches, "Id", "Id", playerInMatch.MatchId);

            var match = await _context.Matches
                             .Include(m => m.Guest)
                             .Include(m => m.Host).Include(m => m.Players).ThenInclude(pm => pm.Player)
                             .FirstOrDefaultAsync(m => m.Id == playerInMatch.MatchId);
            List<PlayerInMatch> hostPlayers = new List<PlayerInMatch>();
            List<PlayerInMatch> guestPlayers = new List<PlayerInMatch>();
            foreach (var item in match.Players)
            {
                if (item.Player.TeamId == match.HostId)
                    hostPlayers.Add(item);
                else
                    guestPlayers.Add(item);
            }
            match.HostPlayers = hostPlayers;
            match.GuestPlayers = guestPlayers;
            return RedirectToAction("Details", "Matches", new { id = match.Id.ToString() });
        }
        /*
        // GET: PlayerInMatches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerInMatch = await _context.PlayerInMatch
                .Include(p => p.Match)
                .Include(p => p.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerInMatch == null)
            {
                return NotFound();
            }

            return View(playerInMatch);
        }

        // POST: PlayerInMatches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playerInMatch = await _context.PlayerInMatch.FindAsync(id);
            _context.PlayerInMatch.Remove(playerInMatch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */
        private bool PlayerInMatchExists(int id)
        {
            return _context.PlayerInMatch.Any(e => e.Id == id);
        }
    }
}
