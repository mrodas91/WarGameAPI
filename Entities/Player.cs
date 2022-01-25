using System;
using System.ComponentModel.DataAnnotations;

namespace WarGameAPI.Entities
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        
        public int WonGames { get; set; }
        
        public int LostGames { get; set; }
    }
}
