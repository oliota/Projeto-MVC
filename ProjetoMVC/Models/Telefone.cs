using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoMVC.Models
{
    public class Telefone
    {
        public int Id { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "O campo numero é obrigatório.")]
        public string Numero { get; set; }
        public string Fornecedor { get; set; }
    }
}