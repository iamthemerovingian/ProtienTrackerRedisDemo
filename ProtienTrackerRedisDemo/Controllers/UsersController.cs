using ProtienTrackerRedisDemo.Models;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProtienTrackerRedisDemo.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult NewUser()
        {
            return View();
        }

        public ActionResult Save(string userName, int goal)
        {
            using (IRedisClient client = new RedisClient(new RedisEndpoint { Host = "117.20.40.28", Port = 6379, Password = "Yellow889" }))
            {
                var userClient = client.As<User>();
                var user = new User
                {
                    Name = userName,
                    Goal = goal,
                    Id = userClient.GetNextSequence()
                };

                userClient.Store(user);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}