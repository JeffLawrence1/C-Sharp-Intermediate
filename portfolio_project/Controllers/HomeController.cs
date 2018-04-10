using Microsoft.AspNetCore.Mvc;


namespace portfolio_project{
        public class HomeController : Controller{
        [HttpGet]
        [Route("")]
        public IActionResult Home(){
            return View("Index");
        }
        [HttpGet]
        [Route("/projects")]
        public IActionResult Projects(){
            return View();
        }
        [HttpGet]
        [Route("/contact")]
        public IActionResult Contact(){
            return View();
        }
        }
}