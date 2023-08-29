using GolfAPI.DAL;
using GolfAPI.DAL.DAO;
using GolfAPI.DAL.DomainClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GolfAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreHoleController : ControllerBase
    {
        readonly AppDbContext db;

        public ScoreHoleController(AppDbContext db)
        {
            this.db = db;
        }

        [Route("{scoreId}")]
        [HttpGet]
        public async Task<ActionResult<List<ScoreHole>>> Index(int scoreId)
        {
            ScoreHoleDAO dao = new(db);
            List<ScoreHole> scoreHoles = await dao.GetScoreHolesByScore(scoreId);
            return Ok(scoreHoles);
        }
    }
}
