using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RVT_WebTerminal.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Necesar de completat spațiu")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Format invalid")]
        [StringLength(13, MinimumLength =13, ErrorMessage = "Lungimea codului personal este din 13 cifre")]
        public string IDNP { get; set; }
        [Required(ErrorMessage = "Necesar de completat spațiu")]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Necesar de completat spațiu")]
        [MaxLength(255)]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Necesar de completat spațiu")]
        [MaxLength(20)]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Necesar de completat spațiu")]
        [MaxLength(255)]
        [MinAge(ErrorMessage= "Trebuie să ai 18 ani sau mai mult.")]
        public string Birth_date { get; set; }
        [Required(ErrorMessage = "Necesar de completat spațiu")]
        [MaxLength(255)]
        public string Region { get; set; }
        [Required(ErrorMessage = "Necesar de completat spațiu")]
        [MaxLength(255)]
        [RegularExpression("^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\\s\\./0-9]*$")]
        public string Phone_Number { get; set; }
        [Required(ErrorMessage = "Necesar de completat spațiu")]
        [EmailAddress(ErrorMessage = "Nu este valid formatul email")]
        public string Email { get; set; }
    }

    public class MinAgeAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            if (DateTime.Today.AddYears(-18) >= Convert.ToDateTime(value))
                return true; 
            return false;
        }
    }
}
