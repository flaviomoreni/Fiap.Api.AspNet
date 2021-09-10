using System;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.AspNet.ViewModel
{
    public class ProdutoVM
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome do produto é requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "NOME deve ser entre 2 e 50 caracteres")]
        public String Nome { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "SKU inválido!")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "SKU inválido")]
        public String Sku { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N2}")]
        public Decimal Preco { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime DataLancamento { get; set; }

        [Required(ErrorMessage = "Categoria é obrigatório")]
        public int CategoriaId { get; set; }
        public CategoriaVM Categoria { get; set; }

        [Required(ErrorMessage = "Marca é obrigatório")]
        public int MarcaId { get; set; }
        public MarcaVM Marca { get; set; }


    }

}
