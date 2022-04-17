using System;
using Microsoft.EntityFrameworkCore;

namespace DbHotel
{
    public class HotelContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<ConveniencesPrice> ConveniencesPrices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=hotel-project;Username=postgres;Password=POP2808qwe");
        }

        public void CreateDbIfNotExist()
        {
            this.Database.EnsureCreated();
        }
    }
}