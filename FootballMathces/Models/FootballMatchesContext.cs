using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballMathces.Models;
using FootballMatches.Models;
using System.Threading;

namespace FootballMathces.Models
{
    public class FootballMatchesContext : DbContext
    {
        public FootballMatchesContext(DbContextOptions<FootballMatchesContext> options) : base(options)
        {

        }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<FootballMatches.Models.PlayerInMatch> PlayerInMatch { get; set; }

          public override int SaveChanges()
          {
             var matches = Matches.ToList();
              foreach (var m in matches)
              {
                  DateTime starOfMatch = m.Time;
                 if (starOfMatch.CompareTo(DateTime.Now) < 0 && m.Status == Match.StatusOfMatch.NotStarted)
                  {
                      m.Status = Match.StatusOfMatch.InProgress;
                  }
              }
              return base.SaveChanges();
          }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var matches = Matches.ToList();
            foreach (var m in matches)
            {
                DateTime starOfMatch = m.Time;
               if (starOfMatch.CompareTo(DateTime.Now) < 0 && m.Status == Match.StatusOfMatch.NotStarted)
                {
                    m.Status = Match.StatusOfMatch.InProgress;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}