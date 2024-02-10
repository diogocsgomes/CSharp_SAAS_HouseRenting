using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trabalho_Pratico_23_24.Models
{
    public class Funcionario 
    {

        [Key]
        public int FuncionarioId { get; set; }
        public String Nome { get; set; }
        public String Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public int N_telefone { get; set; }
        public bool Ativo { get; set; }


        [ForeignKey("LocadorId")]
        public virtual Locador? Locador { get; set; }

        [ForeignKey("UtilizadorId")]
        public virtual ApplicationUser? ApplicationUser { get; set; }

    }
}
