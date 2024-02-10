using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trabalho_Pratico_23_24.Models
{
    public class Gestor //: ApplicationUser
    {
        // Navegação para Locador

        [Key]
        public int gestorId { get; set; }
        public String PrimeiroNome { get; set; }
        public String UltimoNome { get; set; }
        public String Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public int N_telefone { get; set; }
        public bool Ativo { get; set; }

        [ForeignKey("UtilizadorId")]
        public virtual ApplicationUser? ApplicationUser { get; set; }

        [ForeignKey("LocadorId")]
        public virtual Locador? Locador { get; set; }

        
    }
}
