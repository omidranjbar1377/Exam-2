using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_2.Models
{
    public class User
    {
        public int Id { get; set; }
        public bool IsMechanic { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>();
    }

    public class Car
    {
        public int Id { get; set; }
        public int? MechanicId { get; set; }
        public string Model { get; set; }
        public List<int> VisitIds { get; set; } = new List<int>();
    }

    public class Visit
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public long Ticks { get; set; }
    }
}