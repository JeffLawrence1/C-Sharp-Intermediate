using Microsoft.AspNetCore.Mvc;

namespace callingCard_project{
    public class HomeController : Controller{
        [HttpGet]
        [Route("/{FirstName}/{LastName}/{Age}/{Color}")]
        public JsonResult Index(string FirstName, string LastName, int Age, string Color){
            return Json(new{FirstName = FirstName, LastName = LastName, Age = Age, Color = Color});
        }
    }
}