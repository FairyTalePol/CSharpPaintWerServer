using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaintServer.Database
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Required]
        [RegularExpression("^[A-Za-z](-?[A-Za-z]{1,14})?-?([A-Za-z]{1,15})?$", ErrorMessage ="Invalid first name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z](-?[A-Za-z]{1,14})?-?([A-Za-z]{1,15})?$", ErrorMessage = "Invalid first name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("^[0 - 9A - Za - z] + (.?[0 - 9A - Za - z] +){2, 29}.?[0 - 9A - Za - z]+@[a-z]+.[a-z]{2,4}$", ErrorMessage ="Invalid email")]
        public string Email { get; set; }

        [Required]
        [StringLength(30,MinimumLength =6,ErrorMessage ="The input password must contain from 6 to 30 characters.")]
        public string UserPassword { get; set; }
        
        public UserStatistics Statistics { get; set; }

        public ICollection<Pictures> Pictures { get; set; }
    }
}