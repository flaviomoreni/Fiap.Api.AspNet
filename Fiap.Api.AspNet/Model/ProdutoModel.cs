using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Api.AspNet.Models
{
    [Table("Produtos")]
    public class ProdutoViewModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProdutoId { get; set; }

        [Required]
        [MaxLength(50)]
        public String Nome { get; set; }

        [Required]
        [MaxLength(10)]
        [MinLength(2)]
        public String Sku { get; set; }

        [Required]
        [MaxLength(300)]
        [MinLength(5)]
        public String Descricao { get; set; }

        [Required]
        public Decimal Preco { get; set; }

        [MaxLength(3000)]
        public String Caracteristicas { get; set; }

        public DateTime DataLancamento { get; set; }

        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public CategoriaModel Categoria { get; set; }


        public int MarcaId { get; set; }
        [ForeignKey("MarcaId")]
        public MarcaModel Marca { get; set; }




        public ProdutoViewModel()
        {
        }

        public ProdutoViewModel(int ProdutoId, String Nome)
        {
            this.ProdutoId = ProdutoId;
            this.Nome = Nome;
        }

        public ProdutoViewModel(int produtoId, string nome, string sku, string descricao, decimal preco, string caracteristicas, DateTime dataLancamento, int categoriaId, int marcaId) : this(produtoId, nome)
        {
            Sku = sku;
            Descricao = descricao;
            Preco = preco;
            Caracteristicas = caracteristicas;
            DataLancamento = dataLancamento;
            CategoriaId = categoriaId;
            MarcaId = marcaId;
        }
    }
}
