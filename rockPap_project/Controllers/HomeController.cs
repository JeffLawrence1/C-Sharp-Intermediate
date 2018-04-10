using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace rockPap_project{
    public class HomeController : Controller{
        private static Random random = new Random();
        
        [HttpGet]
        [Route("")]
        public IActionResult Index(){

            int? gamesPlayed = HttpContext.Session.GetInt32("gamesPlayed");
            if (gamesPlayed == null) {
                gamesPlayed = 0;
                HttpContext.Session.SetInt32("gamesPlayed", (int) gamesPlayed);
            }
            int? gamesWon = HttpContext.Session.GetInt32("gamesWon");
            if (gamesWon == null) {
                gamesWon = 0;
                HttpContext.Session.SetInt32("gamesWon", (int) gamesWon);
            }
            int? gamesTied = HttpContext.Session.GetInt32("gamesTied");
            if (gamesTied == null) {
                gamesTied = 0;
                HttpContext.Session.SetInt32("gamesTied", (int) gamesTied);
            }
            ViewBag.gamesTied = gamesTied;
            ViewBag.gamesPlayed = gamesPlayed;
            ViewBag.gamesWon = gamesWon;

            return View();
        }

        [HttpPost]
        [Route("/process")]
        public IActionResult Process(string activity){

            
            int comp = random.Next(1, 4);
            int uPick = 0;

            if (string.Equals("Rock", activity, StringComparison.OrdinalIgnoreCase)){
                uPick = 1;
            }
            if (string.Equals("Paper", activity, StringComparison.OrdinalIgnoreCase)){
                uPick = 2;
            }
            if (string.Equals("Scissors", activity, StringComparison.OrdinalIgnoreCase)){
                uPick = 3;
            }
            int uWin = 0;

            if( comp == uPick){
                uWin = 0;
            }
            else if( comp == 1){
                if(uPick == 2){
                    uWin = 1;
                }
                else if(uPick == 3){
                    uWin = -1;
                }
            }
            else if( comp == 2){
                if(uPick == 1){
                    uWin = -1;
                }
                else if(uPick == 3){
                    uWin = 1;
                }
            }
            else if( comp == 3){
                if(uPick == 1){
                    uWin = 1;
                }
                else if(uPick == 2){
                    uWin = -1;
                }
            }
            int? gamesTied = HttpContext.Session.GetInt32("gamesTied");
            int? gamesWon = HttpContext.Session.GetInt32("gamesWon");
            if( uWin == -1){
                ViewBag.Message = "You suck and lost to a stupid computer!!!!";
            }
            else if( uWin == 0){
                ViewBag.Message = "Tie!!!!";

                gamesTied = HttpContext.Session.GetInt32("gamesTied");

                gamesTied += 1;
                HttpContext.Session.SetInt32("gamesTied", (int) gamesTied);

            }
            else if( uWin == 1){

                ViewBag.Message = "Winner!!!!!!";
                gamesWon = HttpContext.Session.GetInt32("gamesWon");

                gamesWon += 1;
                HttpContext.Session.SetInt32("gamesWon", (int) gamesWon);
            }

            int? gamesPlayed = HttpContext.Session.GetInt32("gamesPlayed");
            gamesPlayed += 1;
            HttpContext.Session.SetInt32("gamesPlayed", (int) gamesPlayed);

            ViewBag.gamesPlayed = gamesPlayed;
            ViewBag.gamesWon = gamesWon;
            ViewBag.gamesTied = gamesTied;

            return View("Index");

            
        }
    }
}