using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class WorkersController : ControllerBase
    {
        [HttpGet]
        public List<Models.Workers> LookWorkers()
        {
            var db = new ApplicationContext();
            return db.workers_persons.ToList();
        }
        [HttpPost]
        public IActionResult AddWorkers(string fullname, int age, string who)
        {
            var db = new ApplicationContext();
            db.workers_persons.Add(new Models.Workers
            {
                FullName = fullname,
                Age = age,
                Who = who
            });
            db.SaveChanges();

            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateWorkers(int id, string fullname, int age, string who)
        {
            var db = new ApplicationContext();
            var row = db.workers_persons.FirstOrDefault(x => x.Id == id);
            if (row == null)
            {
                return BadRequest("no worker");
            }
            row.FullName = fullname;
            row.Age = age;
            row.Who = who;
            db.SaveChanges();

            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteWorkers(int id)
        {
            var db = new ApplicationContext();
            var row = db.workers_persons.FirstOrDefault(x => x.Id == id);
            if (row == null)
            {
                return BadRequest("no worker");
            }
            db.workers_persons.Remove(row);
            db.SaveChanges();

            return Ok();
        }
    }
}
