using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace dojodachi_project{
    
    public class HomeController : Controller{
        private static Random random = new Random();

        [HttpGet]
        [Route("")]
        public IActionResult Index(){
            if(HttpContext.Session.GetObjectFromJson<Dachi>("Dojodachi") == null){
                HttpContext.Session.SetObjectAsJson("Dojodachi", new Dachi());
            }
            ViewBag.pDachi = HttpContext.Session.GetObjectFromJson<Dachi>("Dojodachi");
            ViewBag.Status = "start";
            ViewBag.React = "Happy!!!!!";
            ViewBag.Message = "A new Dachi is born!!!";

            return View();
        }

        [HttpPost]
        [Route("/process")]
        public IActionResult Process(string activity){

            Dachi updateDachi = HttpContext.Session.GetObjectFromJson<Dachi>("Dojodachi");



            if(updateDachi.Fullness > 35 && updateDachi.Fullness < 75 || updateDachi.Happiness > 30 && updateDachi.Happiness < 75){
                ViewBag.Status = "mid";
            }
            else if(updateDachi.Fullness >= 75 || updateDachi.Happiness >= 75){
                ViewBag.Status = "high";
            }
            else{
                ViewBag.Status = "start";
            }

            if (string.Equals("Feed", activity, StringComparison.OrdinalIgnoreCase) && updateDachi.Meals >0){
                updateDachi.Meals -= 1;
                int f1 = random.Next(5, 11);
                int f2 = random.Next(0, 4);
                if(f2 < 3){
                    updateDachi.Fullness += f1;
                    ViewBag.React = "Happy!!!!!";
                    ViewBag.Message = $"You fed your Dachi, increasing its fullness by {f1}!!!";
             
                    
                }
                else if(f2 == 3){
                    ViewBag.React = "Disgusted!!!!";
                    ViewBag.Message = "You fed your Dachi, but the food was rotten!!!!";
                 
                }
                
            }
            else if(string.Equals("Feed", activity, StringComparison.OrdinalIgnoreCase) && updateDachi.Meals <= 0){
                ViewBag.React = "Thinks your so lazy!!!!";
                ViewBag.Message = "Go put in some work you don't have any meals for your Dachi";
       
            }
            if (string.Equals("Play", activity, StringComparison.OrdinalIgnoreCase) && updateDachi.Energy >0){
                updateDachi.Energy -= 5;
                int f1 = random.Next(5, 11);
                int f2 = random.Next(0, 4);

                if(f2 < 3){
                    updateDachi.Happiness += f1;
                    ViewBag.React = "Happy!!!!!";
                    ViewBag.Message = $"You played with your Dachi, increasing its happiness by {f1}!!!";

                }
                else if(f2 == 3){
                    ViewBag.React = "Pissed!!!!";
                    ViewBag.Message = "Your boring and your Dachi doesn't like playing with you!!!!";
                }
            }
            else if(string.Equals("Play", activity, StringComparison.OrdinalIgnoreCase) && updateDachi.Energy <= 0){
                ViewBag.React = "Thinks your working too hard!!!!";
                ViewBag.Message = "Go get some sleep so you have energy to play with your Dachi";
            }
            if (string.Equals("Work", activity, StringComparison.OrdinalIgnoreCase) && updateDachi.Energy > 0){
                updateDachi.Energy -= 5;
                int f3 = random.Next(1, 4);
                updateDachi.Meals += f3;
                ViewBag.React = "Happy you work so hard to provide for Dachi!!";
                ViewBag.Message = $"You earned {f3} meals for your Dachi.";
            }
            // else if(string.Equals("Work", activity, StringComparison.OrdinalIgnoreCase) && updateDachi.Energy <= 0){
            //     ViewBag.React = "Thinks your working too hard!!!!";
            //     ViewBag.Message = "Go get some sleep so you have energy to work for your Dachi";
            // }
            if (string.Equals("Sleep", activity, StringComparison.OrdinalIgnoreCase)){
                updateDachi.Energy += 15;
                updateDachi.Fullness -= 5;
                updateDachi.Happiness -= 5;
                ViewBag.React = "Rested";
                ViewBag.Message = "Your Dachi had a good nights sleep.";
            }
            // else if (string.Equals("Sleep", activity, StringComparison.OrdinalIgnoreCase) && updateDachi.Fullness <= 5 || updateDachi.Happiness <= 5){
            //     ViewBag.React = "Horrified";
            //     ViewBag.Message = "If your Dachi goes to sleep right now it will never wake up!!!! You tryin to kill Dachi?";
            // }

            HttpContext.Session.SetObjectAsJson("Dojodachi", updateDachi);
            ViewBag.pDachi = updateDachi;

            if(updateDachi.Fullness > 100 && updateDachi.Happiness > 100 && updateDachi.Energy > 100){
                HttpContext.Session.SetObjectAsJson("Dojodachi", updateDachi);
                ViewBag.pDachi = updateDachi;
                ViewBag.Message = "!!!!!!!!!You Win!!!!!!!";
                return View("Win");

            }
            if(updateDachi.Fullness <= 0 || updateDachi.Happiness <= 0){
                HttpContext.Session.SetObjectAsJson("Dojodachi", updateDachi);
                ViewBag.pDachi = updateDachi;
                ViewBag.Message = "!!!!!!!!!You Lose, Your Dachi is Dead!!!!!!!";
                return View("Lose");
            }

            return View("Index");
        }
        [HttpPost]
        [Route("/restart")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
    // Somewhere in your namespace, outside other classes
    public static class SessionExtensions
{
    // We can call ".SetObjectAsJson" just like our other session set methods, by passing a key and a value
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            // This helper function simply serializes theobject to JSON and stores it as a string in session
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        
        // generic type T is a stand-in indicating that we need to specify the type on retrieval
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            // Upon retrieval the object is deserialized based on the type we specified
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
}
}