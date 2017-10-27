using ProtienTrackerRedisDemo.Models;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProtienTrackerRedisDemo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            using (IRedisClient client = new RedisClient(new RedisEndpoint { Host = "117.20.40.28", Port = 6379, Password = "Yellow889" }))
            {
                var userClient = client.As<User>();
                var users = userClient.GetAll();
                var userSelection = new SelectList(users, "Id", "Name", string.Empty);

                ViewBag.Users = userSelection;
            }
            return View();
        }
    }
}