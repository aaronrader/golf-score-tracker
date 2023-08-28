using GolfAPI.DAL.DAO;
using GolfAPI.DAL.DomainClasses;
using GolfAPI.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GolfAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        readonly AppDbContext db;

        public ScoreController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Score>>> Index()
        {
            ScoreDAO dao = new(db);
            List<Score> scores = await dao.GetAll();
            return Ok(scores);
        }
    }
}
