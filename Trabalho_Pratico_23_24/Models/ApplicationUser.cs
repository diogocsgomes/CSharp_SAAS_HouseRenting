using Microsoft.AspNetCore.Identity;
using System;

namespace Trabalho_Pratico_23_24.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? PrimeiroNome { get; set; }
        public string? UltimoNome { get; set; }
        public DateTime?  DataNascimento { get; set; }
        public int? NIF { get; set; }
    }
}
