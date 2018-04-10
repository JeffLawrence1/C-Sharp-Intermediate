using Microsoft.AspNetCore.Mvc;

namespace timeDisplay_project{
        public class HomeController : Controller{
        [HttpGet]
        [Route("")]
        public IActionResult Index(){
            return View();
        }
        }
}