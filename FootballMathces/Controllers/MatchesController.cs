using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FootballMathces.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.CodeAnalysis.CSharp;
using FootballMatches.Models;
using FootballMatches.Validator;
using FluentValidation.AspNetCore;
using FootballMathces;

namespace FootballMatches.Controllers
{
    public class MatchesController : Controller
    {
        private readonly FootballMatchesContext _context;

        public MatchesController(FootballMatchesContext context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index(int? pageNumber)
        {
            await _context.SaveChangesAsync();
            var footballMatchesContext = _context.Matches.Include(m => m.Guest).Include(m => m.Host);
            int pageSize = 5;
            
            return View(await PaginatedList<Match>.CreateAsync(footballMatchesContext, pageNumber ?? 1, pageSize));
            
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.Guest)
                .Include(m => m.Host).Include(m => m.Players).ThenInclude(pm => pm.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            List<PlayerInMatch> hostPlayers = new List<PlayerInMatch>();
            List<PlayerInMatch> guestPlayers = new List<PlayerInMatch>();
            foreach (var item in match.Players)
            {
                if (item.Player.TeamId == match.HostId)
                    hostPlayers.Add(item);
                else
                    guestPlayers.Add(item);
            }
            if (match == null)
            {
                return NotFound();
            }
            match.HostPlayers = hostPlayers;
            match.GuestPlayers = guestPlayers;
            return View(match);
        }

        // GET: Matches/Create
        public async Task<IActionResult> Create()
        {

            Team host = await _context.Teams
              .Include(t => t.Players)
              .FirstOrDefaultAsync();
            Team guest = await _context.Teams
              .Include(t => t.Players)
              .FirstOrDefaultAsync(m => m.Id == 2);
            ViewData["GuestId"] = new SelectList(_context.Teams, "Id", "Name", guest.Id);
            ViewData["HostId"] = new SelectList(_context.Teams, "Id", "Name", host.Id);

            List<PlayerInMatch> hostPlayers = new List<PlayerInMatch>();
            List<PlayerInMatch> guestPlayers = new List<PlayerInMatch>();
            foreach (var item in host.Players)
            {
                if (item.Deleted == false) { 
                PlayerInMatch p = new PlayerInMatch { PlayerId = item.Id, Player = item };

                hostPlayers.Add(p);
            }
            
            }
            foreach (var item in guest.Players)
            {
                if (item.Deleted == false)
                {
                    PlayerInMatch p = new PlayerInMatch { PlayerId = item.Id, Player = item };

                    guestPlayers.Add(p);
                }
            }
            Match match = new Match { GuestPlayers = guestPlayers, HostPlayers = hostPlayers, GuestId = guest.Id, HostId = host.Id };


            return View(match);
        }

        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string[] playersHost, string[] playersGuest, [Bind("Id,HostId,GuestId,Time,Place,HostPlayers,GuestPlayers")] Match match)
        {
            Team host = await _context.Teams
                                      .Include(t => t.Players)
                                      .FirstOrDefaultAsync(m => m.Id == match.HostId);
            Team guest = await _context.Teams
              .Include(t => t.Players)
              .FirstOrDefaultAsync(m => m.Id == match.GuestId);
            Match m = _context.Matches.FirstOrDefault(m => ((m.GuestId == match.GuestId || m.HostId == match.HostId) && m.Time.Date.Equals(match.Time.Date)));

            List<PlayerInMatch> hostPlayers = new List<PlayerInMatch>();
            List<PlayerInMatch> guestPlayers = new List<PlayerInMatch>();
            List<PlayerInMatch> selectedHostPlayers = new List<PlayerInMatch>();
            List<PlayerInMatch> selectedGuestPlayers = new List<PlayerInMatch>();
            foreach (var item in host.Players)
            {
                if (item.Deleted == false)
                {
                    PlayerInMatch p = new PlayerInMatch { PlayerId = item.Id, Player = item };
                    if (playersHost.Contains(p.PlayerId.ToString()))
                    {
                        p.IsChecked = true;
                        p.Player.Matches++;
                        selectedHostPlayers.Add(p);
                    }
                    hostPlayers.Add(p);
                }
            }
            foreach (var item in guest.Players)
            {
                if (item.Deleted == false)
                {
                    PlayerInMatch p = new PlayerInMatch { PlayerId = item.Id, Player = item };
                    if (playersGuest.Contains(p.PlayerId.ToString()))
                    {
                        p.IsChecked = true;
                        p.Player.Matches++;
                        selectedGuestPlayers.Add(p);
                    }

                    guestPlayers.Add(p);
                }
            }
            match.GuestPlayers = selectedGuestPlayers;
            match.HostPlayers = selectedHostPlayers;
            MatchesValidator mv = new MatchesValidator();
            var result = mv.Validate(match);
            ModelState.Clear();
            if (m != null)
            {
                ModelState.AddModelError("Time", "Match on this day is already arranged");
            }
            else
            {
                m = null;
            }
            
            result.AddToModelState(ModelState, null);
            if (ModelState.IsValid && m==null)
            {
                match.Players = selectedGuestPlayers;
                foreach (PlayerInMatch item in selectedHostPlayers)
                {
                    match.Players.Add(item);
                }
                _context.Add(match);
                await _context.SaveChangesAsync();

            }
            ViewData["GuestId"] = new SelectList(_context.Teams, "Id", "Name", match.GuestId);
            ViewData["HostId"] = new SelectList(_context.Teams, "Id", "Name", match.HostId);
            ViewData["Result"] = (result.IsValid && m==null).ToString();
            match.GuestPlayers = guestPlayers;
            match.HostPlayers = hostPlayers;



            return View(match);
        }
        // POST: Matches/Finish/5
        [HttpPost, ActionName("Finish")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Finish(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            match.Status=Match.StatusOfMatch.Finished;
            var h = _context.Teams.FirstOrDefault(t => t.Id == match.HostId);
            var g = _context.Teams.FirstOrDefault(t => t.Id == match.GuestId);
            if (match.HostGoals > match.GuestGoals)
            {
                h.Wins++;
                g.Losses++;
            }
            else if (match.HostGoals < match.GuestGoals)
            {
                g.Wins++;
                h.Losses++;
            }
            else
            {
                g.Draws++;
                h.Draws++;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        // POST: Matches/Cancel/5
        public async Task<IActionResult> Cancel(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            match.Status = Match.StatusOfMatch.Canceled;
            //_context.Matches.Remove(match);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<object> Edit(string[] playersHost, string[] playersGuest, [Bind("Id,HostId,GuestId,Time,Place,HostPlayers,GuestPlayers")] Match match)
        {
            Team host = await _context.Teams
                                      .Include(t => t.Players)
                                      .FirstOrDefaultAsync(m => m.Id == match.HostId);
            Team guest = await _context.Teams
              .Include(t => t.Players)
              .FirstOrDefaultAsync(m => m.Id == match.GuestId);

            List<PlayerInMatch> hostPlayers = new List<PlayerInMatch>();
            List<PlayerInMatch> guestPlayers = new List<PlayerInMatch>();

            foreach (var item in host.Players)
            {
                if (item.Deleted == false)
                {
                    PlayerInMatch p = new PlayerInMatch { PlayerId = item.Id, Player = item };
                    if (playersHost.Contains(p.PlayerId.ToString()))
                    {
                        p.IsChecked = true;
                        //selectedHostPlayers.Add(p);
                    }
                    hostPlayers.Add(p);
                }
            }
            foreach (var item in guest.Players)
            {
                if (item.Deleted == false)
                {
                    PlayerInMatch p = new PlayerInMatch { PlayerId = item.Id, Player = item };
                    if (playersGuest.Contains(p.PlayerId.ToString()))
                    {
                        p.IsChecked = true;
                        //selectedGuestPlayers.Add(p);
                    }

                    guestPlayers.Add(p);
                }
            }
            
            ViewData["GuestId"] = new SelectList(_context.Teams, "Id", "Name", match.GuestId);
            ViewData["HostId"] = new SelectList(_context.Teams, "Id", "Name", match.HostId);
            ModelState.Clear();
            match.GuestPlayers = guestPlayers;
            match.HostPlayers = hostPlayers;
            return PartialView("Create", match);
        }
        /*
        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.Guest)
                .Include(m => m.Host)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */
        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.Id == id);
        }
    }
}
