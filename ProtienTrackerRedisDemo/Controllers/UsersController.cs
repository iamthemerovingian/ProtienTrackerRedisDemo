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

        public ActionResult Save(string userName, int goal, long? userId)
        {
            using (IRedisClient client = new RedisClient(new RedisEndpoint { Host = "117.20.40.28", Port = 6379, Password = "Yellow889" }))
            {
                var userClient = client.As<User>();
                User user;

                if (userId != null)
                {
                    user = userClient.GetById(userId);
                    client.RemoveItemFromSortedSet("urn:leaderboard", user.Name);
                }
                else
                {
                    user = new User
                    {
                        Id = userClient.GetNextSequence()
                    };
                }

                user.Name = userName;
                user.Goal = goal;
                userClient.Store(user);
                userId = user.Id;
                client.AddItemToSortedSet("urn:leaderboard", user.Name, user.Total);

            }

            return RedirectToAction("Index", "Tracker", new { userId });
        }

        public ActionResult Edit(long userid)
        {
            using (IRedisClient client = new RedisClient(new RedisEndpoint { Host = "117.20.40.28", Port = 6379, Password = "Yellow889" }))
            {
                var userClient = client.As<User>();
                var user = userClient.GetById(userid);

                ViewBag.UserName = user.Name;
                ViewBag.Goal = user.Goal;
                ViewBag.UserId = user.Id;
            }

            return View("NewUser");
        }

        public ActionResult Delete(long userid)
        {
            using (IRedisClient client = new RedisClient(new RedisEndpoint { Host = "117.20.40.28", Port = 6379, Password = "Yellow889" }))
            {
                var userClient = client.As<User>();
                var user = userClient.GetById(userid);

                //Set a flag for deleted User.
                user.IsDeleted = true;
                userClient.Store(user);

                //Set a flag for deleted leaderboard entry.
                client.RemoveItemFromSortedSet("urn:leaderboard", user.Name);


                //Set a flag for deleted history.
                var historyClient = client.As<int>();
                var historyList = historyClient.Lists["urn:history:" + userid];
                historyList.Clear();


                ViewBag.UserName = string.Empty;
                ViewBag.Goal = string.Empty;
                ViewBag.UserId = string.Empty;
            }

            return View("NewUser");
        }
    }
}