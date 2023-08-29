using GolfAPI.DAL.DomainClasses;
using Microsoft.EntityFrameworkCore;

namespace GolfAPI.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public virtual DbSet<Course>? Courses { get; set; }
        public virtual DbSet<Hole>? Holes { get; set; }
        public virtual DbSet<Score>? Scores { get; set; }
        public virtual DbSet<ScoreHole>? ScoreHoles { get; set; }
    }
}
