using PaintServer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaintServer.Entities
{
    public class PictureData
    {
        public int UserId { get; set; }
        public PictureType Type { get; set; }
        public string Picture { get; set; }

        public string Name { get; set; }

        public PictureData()
        {

        }
    }
}
