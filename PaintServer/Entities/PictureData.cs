using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaintServer.Entities
{
    public class PictureData
    {
        public int UserId { get; set; }
        public string Type { get; set; }
        public string Picture { get; set; }

        public PictureData()
        {

        }
    }
}
