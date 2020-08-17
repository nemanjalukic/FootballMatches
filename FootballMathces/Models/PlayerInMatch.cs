using FootballMathces.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FootballMatches.Models
{
    public class PlayerInMatch
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("Player")]
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int Goals { get; set; }
        [JsonIgnore]
        public bool IsChecked { get; set; }
        [Required]
        [ForeignKey("Match")]
        public int MatchId { get; set; }
        public Match Match {get;set;}

        public void AddGoal()
        {
            Goals++;
            Player.Goals++;
            if (Player.TeamId == Match.HostId)
            {
                Match.HostGoals++;
            }
            else
            {
                Match.GuestGoals++;
            }
        }


    }
}
