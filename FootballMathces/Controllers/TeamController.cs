using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FootballMathces.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace FootballMathces.Controllers
{
    public class TeamController : Controller
    {
        private readonly FootballMatchesContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TeamController(FootballMatchesContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Teams
        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 5;
            //var teams = (await _context.Teams.ToListAsync());
            return View(await PaginatedList<Team>.CreateAsync(_context.Teams.Include(t=>t.Players), pageNumber ?? 1, pageSize));
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                  .Include(s => s.Players)
                .FirstOrDefaultAsync(m => m.Id == id);
            ICollection<Player> players=new List<Player>(); ;
           foreach(var item in team.Players)
            {
                if (item.Deleted == false)
                {
                    players.Add(item);
                }
            }
            team.Players=players;
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            //return View();
            return PartialView("Create", new Team());
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Logo,ImageFile")] Team team)
        {
            if (ModelState.IsValid)
            {
                if (team.ImageFile != null) {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(team.ImageFile.FileName);
                string extension = Path.GetExtension(team.ImageFile.FileName);
                team.Logo = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/img/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await team.ImageFile.CopyToAsync(fileStream);
                }
                }
                _context.Add(team);
                await _context.SaveChangesAsync();
                // return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewTeams", _context.Teams.ToList()) });
                return PartialView("Create", team);
            }
             else
             {
                
                 return PartialView("Create", team);
             }
           

        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Logo,Desc")] Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Delete/5
      /*  public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

        public async Task<ActionResult> StatisticAsync()
        {
            ViewData["Players"] = _context.Player.Include(p=>p.Team).ToList();
            return View(await _context.Teams.ToListAsync());
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}
