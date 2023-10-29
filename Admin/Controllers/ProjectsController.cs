using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class ProjectsController : ControllerBase
    {
        [HttpGet]
        public List<Models.Projects> LookProjects()
        {
            var db = new ApplicationContext();
            return db.us_projects.ToList();
        }
        [HttpPost]
        public IActionResult AddProjects(string name, string image)
        {
            var db = new ApplicationContext();
            db.us_projects.Add(new Models.Projects
            {
                Name = name,
                Image = image
            });
            db.SaveChanges();

            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateProjects(int id, string name, string image)
        {
            var db = new ApplicationContext();
            var row = db.us_projects.FirstOrDefault(x => x.Id == id);
            if (row == null)
            {
                return BadRequest("no worker");
            }
            row.Name = name;
            row.Image = image;
            db.SaveChanges();

            return Ok();
        }
        [HttpDelete]
        public IActionResult Deleteprojects(int id)
        {
            var db = new ApplicationContext();
            var row = db.us_projects.FirstOrDefault(x => x.Id == id);
            if (row == null)
            {
                return BadRequest("no worker");
            }
            db.us_projects.Remove(row);
            db.SaveChanges();

            return Ok();
        }
    }
}