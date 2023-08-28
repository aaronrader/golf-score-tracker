using GolfAPI.DAL;
using GolfAPI.DAL.DAO;
using GolfAPI.DAL.DomainClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GolfAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        readonly AppDbContext db;

        public CourseController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Course>>> Index()
        {
            CourseDAO dao = new(db);
            List<Course> courses = await dao.GetAll();
            return Ok(courses);
        }
    }
}
