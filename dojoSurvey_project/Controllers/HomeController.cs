using Microsoft.AspNetCore.Mvc;

namespace dojoSurvey_project{
        public class HomeController : Controller{
        [HttpGet]
        [Route("")]
        public IActionResult Home(){
            return View("Index");
        }
        [HttpPost]
        [Route("/landing")]
        public IActionResult Landing(string name, string location, string language, string comment){
            ViewBag.name = name;
            ViewBag.location = location;
            ViewBag.language = language;
            ViewBag.comment = comment;
            return View();
        }
        
        }
}