using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.AspNet.ViewModel
{
    public class UsuarioVM
    {

        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string Senha { get; set; }

        public string Regra { get; set; }


    }
}
