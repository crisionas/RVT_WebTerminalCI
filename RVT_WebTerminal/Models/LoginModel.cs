using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RVT_WebTerminal.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Necesar de completat spațiu")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Format invalid")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Lungimea codului personal este din 13 cifre")]
        public string IDNP { get; set; }

        [Required(ErrorMessage = "Necesar de completat spațiu")]
        public string VnPassword { get; set; }
    }
}
