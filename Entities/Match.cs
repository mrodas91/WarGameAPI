using System;
using System.ComponentModel.DataAnnotations;

namespace WarGameAPI.Entities
{
    public class Match
    {
        public int Id { get; set; }
        [Required]
        public int PlayerId1 { get; set; }
        [Required]
        public int PlayerId2 { get; set; }
        [Required]
        public int PlayedCards { get; set; }
        [Required]
        public int Winner { get; set; }
    }
}
