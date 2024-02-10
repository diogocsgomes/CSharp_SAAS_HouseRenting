using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trabalho_Pratico_23_24.Models
{
    public class CheckOut
    {
        [Key]
        public int Id { get; set; }

        public DateTime HoraSaida { get; set; }
        public bool TemEquipamentosOpcionais { get; set; }
        public string? EquipamentoOpcionais { get; set; }
        public bool TemDanos { get; set; }

        public string? Danos { get; set; }

        public string DanosPath { get; set; }

        public string Observacoes { get; set; }
        [ForeignKey("RecetorId")]
        public ApplicationUser? Recetor { get; set; }
    }
}
