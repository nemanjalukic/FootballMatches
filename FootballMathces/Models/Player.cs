using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FootballMathces.Models
{
    public class Player
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Matches { get; set; }
        public int Goals { get; set; }
        [Required]
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public Team Team { get; set; }
        [JsonIgnore]
        public bool Deleted { get; set; }
    }
}
