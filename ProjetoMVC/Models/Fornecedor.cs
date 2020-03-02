using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoMVC.Models
{
    public class Fornecedor
    { 
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public string Nome { get; set; }

        [Display(Name = "Cpf / Cnpj")]
        [Required(ErrorMessage = "O campo Cpf/Cnpj é obrigatório.")]
        public string CpfCnpj { get; set; }

        [Display(Name = "Cadastrado em")]
        public DateTime DataHoraCadastro { get; set; }

        public string Empresa { get; set; }
        public int Tipo { get; set; }


        [Display(Name = "RG")] 
        public string RG { get; set; }


        [Display(Name = "Data de nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataNascimento { get; set; }


        public string Idade { get; set; }

    }
}