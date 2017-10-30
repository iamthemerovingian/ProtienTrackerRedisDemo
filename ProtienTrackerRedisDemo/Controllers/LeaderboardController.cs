using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProtienTrackerRedisDemo.Controllers
{
    public class LeaderboardController : Controller
    {
        // GET: Leaderboard
        public ActionResult Index()
        {
            using (IRedisClient client = new RedisClient(new RedisEndpoint { Host = "117.20.40.28", Port = 6379, Password = "Yellow889" }))
            {
                var leaderboard = client.GetAllWithScoresFromSortedSet("urn:leaderboard");
                ViewBag.Leaders = leaderboard;
            }
                return View();
        }
    }
}