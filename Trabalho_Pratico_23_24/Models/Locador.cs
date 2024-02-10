using System.ComponentModel.DataAnnotations;

namespace Trabalho_Pratico_23_24.Models
{
    public class Locador
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }
        [StringLength(9, ErrorMessage = "O número de telefone deve ter no máximo 9 dígitos.")]
        [Required(ErrorMessage = "O número de telefone é obrigatório.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "O número de telefone deve ter exatamente 9 dígitos.")]
        public string N_Telefone { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }

        public ICollection<Habitacao> Habitacoes { get; set; } = new List<Habitacao>();
    }
}
