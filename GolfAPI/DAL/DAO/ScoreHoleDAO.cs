using GolfAPI.DAL.DomainClasses;
using Microsoft.EntityFrameworkCore;

namespace GolfAPI.DAL.DAO
{
    public class ScoreHoleDAO
    {
        private readonly AppDbContext db;

        public ScoreHoleDAO(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<ScoreHole>> GetScoreHolesByScore(int scoreId)
        {
            var scoreHoles = await db.ScoreHoles!.Where(sc => sc.ScoreId == scoreId).ToListAsync();

            scoreHoles.ForEach(scoreHole =>
            {
                //scoreHole.Score = db.Scores!.Where(score => score.Id == scoreHole.ScoreId).FirstOrDefault();
                scoreHole.Hole = db.Holes!.Where(hole => hole.Id == scoreHole.HoleId).FirstOrDefault();
            });

            return scoreHoles;
        }
    }
}
