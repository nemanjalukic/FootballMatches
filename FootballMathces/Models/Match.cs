using FootballMatches.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FootballMathces.Models
{
    public class Match
    {
        public enum StatusOfMatch
        {
            NotStarted,
            Finished,
            Canceled,
            InProgress

        }
        public int Id { get; set; }
        [Required]
        [ForeignKey("Host")]
        public int HostId { get; set; }
        public Team Host { get; set; }
        [Required]
        [ForeignKey("Guest")]
        public int GuestId { get; set; }
        public Team Guest { get; set; }
        public DateTime Time { get; set; }
        public string Place { get; set; }
        public int HostGoals { get; set; }
        public int GuestGoals { get; set; }
        public  StatusOfMatch Status { get; set; }
        [JsonProperty("goal_scorers")]
        public ICollection<PlayerInMatch> Players { get; set; }
        [NotMapped]
        [BindProperty]
        public List<PlayerInMatch> HostPlayers { get; set; } = new List<PlayerInMatch>();
        [NotMapped]
        [BindProperty]
        public List<PlayerInMatch> GuestPlayers { get; set; } = new List<PlayerInMatch>();

        public Match()
        {
           Status = StatusOfMatch.NotStarted;
        }
    }
}
