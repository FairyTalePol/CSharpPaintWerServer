using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PaintServer.Database
{
    public class Pictures
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public int UserId { get; set; }

        public string Name { get; set; }

        public User User { get; set; }
        public string PictureType { get; set; }
        public string Picture { get; set; } //Либо json, либо сериализованная картинка

       
    }
}
