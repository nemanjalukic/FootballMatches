using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FootballMathces.Models
{
    public class Team
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Desc { get; set; }
        [NotMapped]
        [DisplayName("Logo")]
        public IFormFile ImageFile { get; set; }
        [JsonIgnore]
        public ICollection<Player> Players { get; set; }
        [NotMapped]
        public int NumberOfPlayers => GetActivePlayers().Count();
        public IEnumerable<Player> GetActivePlayers()
        {
            return Players.Where(p => p.Deleted == false);
        }

        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }

        public Team()
        {
            Players = new List<Player>();
        }
    }
}
