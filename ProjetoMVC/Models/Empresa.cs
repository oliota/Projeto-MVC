using System.ComponentModel.DataAnnotations;

namespace ProjetoMVC.Models
{
    public class Empresa
    {

        public int Id { get; set; }

        [Display(Name = "Nome fantasia")]
        [Required(ErrorMessage = "O campo nome fantasia é obrigatório.")]
        public string NomeFantasia { get; set; }
        [Required(ErrorMessage = "O campo UF é obrigatório.")]
        public string UF { get; set; }
        [Required(ErrorMessage = "O campo CNPJ é obrigatório.")]
        public string CNPJ { get; set; }
    }
}