using System.ComponentModel.DataAnnotations;

namespace Trabalho_Pratico_23_24.Models
{
    public class Categorias
    {
        [Key]
        public int Id { get; set; }
		[Required]
		public string Nome { get; set; }
		[Required]
		public string Descricao { get; set; }


        public List<Habitacao>? Habitacoes { get; set; }
        public Categorias()
        {

        }

    }
}
