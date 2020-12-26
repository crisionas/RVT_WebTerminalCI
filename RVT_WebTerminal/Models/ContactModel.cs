using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RVT_WebTerminal.Models
{
    public class ContactModel
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "Te rugăm să introduci o adresă de email validă.")]
        [EmailAddress(ErrorMessage = "Te rugăm să introduci o adresă de email validă.")]
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
