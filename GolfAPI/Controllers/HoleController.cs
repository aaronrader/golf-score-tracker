using GolfAPI.DAL;
using GolfAPI.DAL.DAO;
using GolfAPI.DAL.DomainClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GolfAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoleController : ControllerBase
    {
        private readonly AppDbContext db;

        public HoleController(AppDbContext db)
        {
            this.db = db;
        }

        [Route("{courseId}")]
        [HttpGet]
        public async Task<ActionResult<List<Hole>>> Index(int courseId)
        {
            HoleDAO dao = new(db);
            List<Hole> holes = await dao.GetHolesByCourse(courseId);
            return Ok(holes);
        }
    }
}
