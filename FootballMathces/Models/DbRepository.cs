using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballMathces.Models
{
    public class DbRepository : IFootballMatchesRepository
    {
        private readonly FootballMatchesContext _db;

        public DbRepository(FootballMatchesContext db)
        {
            _db = db;
        }
        public IQueryable<Team> Teams => _db.Teams;

        public IQueryable<Match> Matches => _db.Matches;

        public void Add<EntityType>(EntityType entity) => _db.Add(entity);


        public void SaveChanges() => _db.SaveChanges();
    }
}
