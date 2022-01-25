using System;
using System.ComponentModel.DataAnnotations;

namespace WarGameAPI.DTO
{
    public class PlayersResultsDTO
    {
        [Required]
        public int Id { get; set; }

        public int WonGames { get; set; }

        public int LostGames { get; set; }
    }
}
