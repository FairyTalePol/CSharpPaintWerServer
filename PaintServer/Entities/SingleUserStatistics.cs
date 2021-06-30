using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaintServer.Entities
{
    public class SingleUserStatistics
    { 
        public string Email { get; set; }
        public string AmountBMP { get; set; }
        public string AmountJson { get; set; }
        public string AmountJPG { get; set; }
        public string AmountPNG { get; set; }
        public string AmountTotal { get; set; }
        public string RegistrationDate { get; set; }
        public string LastActivity { get; set; }

        //public SingleUserStatistics()
        //{

        //}
    }
}
