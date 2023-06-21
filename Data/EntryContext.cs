using DynamicSun_weather.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace DynamicSun_weather.Data
{
    public class EntryContext : DbContext
    {
        public EntryContext(DbContextOptions<EntryContext> options)
        : base(options)
        { }
        public DbSet<EntryModel> Entries { get; set; }
    }
}
