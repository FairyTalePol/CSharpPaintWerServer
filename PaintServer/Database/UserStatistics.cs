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

        
        [RegularExpression("^(202[1-9]|2[0-9]{3}).(0[1-9]|1[0-2]).(0[1-9]|1[0-9]|2[0-9]|3[0-1])$")]
        public string RegistrationDate { get; set; }

        [RegularExpression("^(202[1-9]|2[0-9]{3}).(0[1-9]|1[0-2]).(0[1-9]|1[0-9]|2[0-9]|3[0-1]) ([0-1][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])$")]
        public string LastActivity { get; set; }


    }
}
