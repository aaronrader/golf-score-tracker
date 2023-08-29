using GolfAPI.DAL.DomainClasses;
using Microsoft.EntityFrameworkCore;

namespace GolfAPI.DAL.DAO
{
    public class HoleDAO
    {
        private readonly AppDbContext db;

        public HoleDAO(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Hole>> GetHolesByCourse(int courseId)
        {
            var holes = await db.Holes!.Where(hole => hole.CourseId == courseId).ToListAsync();

            holes.ForEach(hole =>
            {
                hole.Course = db.Courses!.Where(course => course!.Id == hole.CourseId).FirstOrDefault();
            });

            return holes;
        }
    }
}
