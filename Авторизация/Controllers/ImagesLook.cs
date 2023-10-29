using Microsoft.AspNetCore.Mvc;

namespace Авторизация.Controllers
{
    public class ImagesLook : ControllerBase
    {
        [HttpGet]
        public List<Models.Images> Look_Photo()
        {
            using (var db = new applicationContext())
            {
                return db.images_table.ToList();
            }
        }
    }
}
