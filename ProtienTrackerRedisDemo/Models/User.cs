using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProtienTrackerRedisDemo.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Total { get; set; }
        public int Goal { get; set; }
        public bool IsDeleted { get; set; }
    }
}