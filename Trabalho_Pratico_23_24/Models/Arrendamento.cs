using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trabalho_Pratico_23_24.Models
{
    public class Arrendamento
    {
        [Key]
        public int Id { get; set; }

        

        [ForeignKey("ClienteId")]
        public ApplicationUser? Cliente { get; set; }

        public int LocadorId { get; set; }

        [ForeignKey("LocadorId")]
        public Habitacao? Imovel { get; set; }

        public DateTime DatraEntrada { get; set; }

        public DateTime DatraSaida { get; set; }

        public Locador? Locador { get; set; }//locador respoinsavel pelo arerendamento
        
        public bool PorConfirmar { get; set; } = false; // Funcionario/Gestor se JA foi aceite ou nao
        
        public bool Aceite { get; set; } = false; // se FOI aceite ou nao
        
        public bool EntreguePorCliente { get; set; } = false; // se o cliente ja entregou a casa ou nao
        
        public bool RecebidoPeloLocador { get; set; } = false; // se a empresa ja efetuou a rececao do imovel

        public bool Ativo { get; set; } = false; // Se o arrendamento esta a decorrer ou nao. Quando o cliente entregar fica a flaso
       
        [ForeignKey("CheckInId")]
       
        public CheckIn? CheckIn { get; set; }
       
        [ForeignKey("CheckOutId")]
        public CheckOut? CheckOut { get; set; }
    }
}
