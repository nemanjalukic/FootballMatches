using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballMathces.Models
{
    public interface IFootballMatchesRepository
    {
        public IQueryable<Team> Teams { get; }
        public IQueryable<Match> Matches { get; }

        void Add<EntityType>(EntityType entity);

        void SaveChanges();
    }
}
