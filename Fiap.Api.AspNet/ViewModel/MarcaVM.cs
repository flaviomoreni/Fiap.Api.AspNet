using System;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.AspNet.ViewModel
{
    public class MarcaVM
    {

        [Key]
        public int MarcaId { get; set; }

        [Required(ErrorMessage = "Marca Obrigatória")]
        [MaxLength(40, ErrorMessage = "Marca deve ter até 40 caracteres")]
        [MinLength(2, ErrorMessage = "Marca deve ter no mínimo 2 caracteres")]
        public String NomeMarca { get; set; }


    }
}
