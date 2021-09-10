using System;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.AspNet.ViewModel
{
    public class CategoriaVM
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "Categoria Obrigatória")]
        [MaxLength(40, ErrorMessage = "Categoria deve ter até 40 caracteres")]
        [MinLength(2, ErrorMessage = "Categoria deve ter no mínimo 2 caracteres")]
        public String NomeCategoria { get; set; }

    }
}
