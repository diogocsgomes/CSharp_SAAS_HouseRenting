using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trabalho_Pratico_23_24.Models
{
    public class Habitacao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Localizacao { get; set; }
        
        [Required]
        [RegularExpression(@"^T[0-9]+$", ErrorMessage = "O Tipo deve começar com 'T' seguido de um número. (T0,T1,T2...)")]
        public string Tipo { get; set; } //T0,T1,T2...
       
        [Required]
        public decimal CustoArrendamento { get; set; }
       
        [Required]
        public DateTime DataInicioContrato { get; set; }
       
        [Required]
        public DateTime DataFimContrato { get; set; }
       
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "O Periodo Minimo de Arrendamento deve ser no mínimo 1.")]
        public int PeriodoMinimoArrendamento { get; set; }
       
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "O Periodo Maximo de Arrendamento deve ser no mínimo 1.")]
        public int PeriodoMaximoArrendamento { get; set; }
       
        [Required]
        [Range(1, 5, ErrorMessage = "A Avaliação do Locador deve estar entre 1 e 5.")]
        public decimal AvaliacaoLocador { get; set; }
       
        [Required]
        //Categoria
        public int? CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]

        public Categorias? Categoria { get; set; }

        public bool Ativo { get; set; }

        [ForeignKey("LocadorId")]
        public virtual List<Locador>? Locadores { get; set; } = new List<Locador>();

        //// Estado da habitação (ativo, inativo, etc.)
        //public bool Estado { get; set; }

        //// Equipamentos opcionais na habitação
        //public List<EquipamentoOpcional> EquipamentosOpcionais { get; set; }

        //// Danos na habitação
        //public string Danos { get; set; }

        //// Observações sobre a habitação
        //public string Observacoes { get; set; }

        public Habitacao()
        {

        }
    }
}
