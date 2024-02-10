using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trabalho_Pratico_23_24.Models;

namespace Trabalho_Pratico_23_24.ViewModels
{
    public class CheckOutViewModel
    {
            [Key]
            public int Id { get; set; }

            public DateTime HoraSaida { get; set; }
            public bool TemEquipamentosOpcionais { get; set; }
            public string? EquipamentoOpcionais { get; set; }
            public bool TemDanos { get; set; }

            public string? Danos { get; set; }

            public IFormFile DanosImagem { get; set; }

            public string Observacoes { get; set; }
            [ForeignKey("RecetorId")]
            public ApplicationUser? Recetor { get; set; }
    }
}
