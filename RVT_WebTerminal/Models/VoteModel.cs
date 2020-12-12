using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RVT_WebTerminal.Models
{
    public class VoteModel
    {
        [Required(ErrorMessage = "Necesar de completat spatiu")]
        public string Party { get; set; }
    }
}
