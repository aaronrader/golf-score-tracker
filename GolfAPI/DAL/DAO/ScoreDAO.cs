using GolfAPI.DAL.DomainClasses;
using Microsoft.EntityFrameworkCore;

namespace GolfAPI.DAL.DAO
{
    public class ScoreDAO
    {
        private readonly AppDbContext db;

        public ScoreDAO(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Score>> GetAll()
        {
            var scores = await db.Scores!.ToListAsync();

            scores.ForEach(score =>
            {
                score.Course = db.Courses!.Where(course => course!.Id == score.CourseId).FirstOrDefault();
            });

            return scores;
        }
    }
}
