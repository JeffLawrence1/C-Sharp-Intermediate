
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace randomPass_project{
    public class HomeController : Controller{
        [HttpGet]
        [Route("")]
        public IActionResult Home(){
            
            int? count = HttpContext.Session.GetInt32("count");
            
            if(count == null){
                count = 0;
            }
            count += 1;



            var random = new Random();
            // Func generator = _=>(char)(int)Math.Floor('Z'-'A' * random.NextDouble() + 'A');
            string poop = string.Join("", Enumerable.Range(1, 15).Select(_=>(char)(int)Math.Floor('Z'-'A' * random.NextDouble() + 'A')));
            ViewBag.poop = poop;
            ViewBag.count = count;
            HttpContext.Session.SetInt32("count", (int)count);
            return View("Index");
        }

    }
}