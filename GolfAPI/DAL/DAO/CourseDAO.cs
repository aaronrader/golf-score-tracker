using GolfAPI.DAL.DomainClasses;
using Microsoft.EntityFrameworkCore;

namespace GolfAPI.DAL.DAO
{
    public class CourseDAO
    {
        private readonly AppDbContext db;

        public CourseDAO(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Course>> GetAll()
        {
            return await db.Courses!.ToListAsync();
        }
    }
}
