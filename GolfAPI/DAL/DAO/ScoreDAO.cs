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

            scores.ForEach( (score) =>
            {
                score.Course = db.Courses!.Where(course => course!.Id == score.CourseId).FirstOrDefault();

                //Get ScoreHole data
                int totalScore = 0;
                List<ScoreHole> scoreHoles = db.ScoreHoles!.Where(sc => sc.ScoreId == score.Id).ToList();
                scoreHoles.ForEach(scoreHole =>
                {
                    scoreHole.Hole = db.Holes!.Where(hole => hole.Id == scoreHole.HoleId).FirstOrDefault();
                    totalScore += (scoreHole.Strokes - scoreHole.Hole!.Par);
                });
                score.TotalScore = totalScore;
            });

            return scores;
        }
    }
}
