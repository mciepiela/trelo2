using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trelo2.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public bool IsReady { get; set; }
        public ApplicationUser User { get; set; }

    }
}