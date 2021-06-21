using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaintServer.Database
{
    public class UserStatistics
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int AmountBMP { get; set; }
        public int AmountJson { get; set; }
        public int AmountTotal { get; set; }

        
       
        public string RegistrationDate { get; set; }

        public string LastActivity { get; set; }


    }
}
