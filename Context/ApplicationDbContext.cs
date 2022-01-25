using System;
using Microsoft.EntityFrameworkCore;
using WarGameAPI.Entities;

namespace WarGameAPI.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
    }
}
