
using BidwotaGiri_Dot_Net_Assignment.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace BidwotaGiri_Dot_Net_Assignment.Data

{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }
        public DbSet<EventsList> EventList {  get; set; }
    }
}
