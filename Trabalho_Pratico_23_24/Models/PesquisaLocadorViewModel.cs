using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Trabalho_Pratico_23_24.Models
{
    public class PesquisaLocadorViewModel
    {
        public List<Locador> ListaDeLocadores { get; set; }
        public int NumResultados { get; set; }

        //[Display(Name = "Texto", Prompt = "introduza o texto a pesquisar")]
        [Display(Name = "Texto", Prompt = "introduza o texto a pesquisar")]

        public String TextoAPesquisar { get; set; }
    }
}
