using Fiap.Api.AspNet.Data;
using Fiap.Api.AspNet.Models;
using Fiap.Api.AspNet.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Fiap.Api.AspNet.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DataContext _context;

        public ProdutoRepository(
            DataContext context)
        {
            _context = context;
        }

        public IList<ProdutoViewModel> FindAll()
        {
            return _context.Produtos.Include(c => c.Categoria)
                                    .Include(m => m.Marca)
                                    .AsNoTracking()
                                    .ToList();
        }

        public ProdutoViewModel FindById(int id)
        {
            return _context.Produtos.Include(c => c.Categoria)
                                    .Include(m => m.Marca)
                                    .FirstOrDefault(x => x.ProdutoId == id);

            //var produtos = from produto in _context.Produtos
            //               join marcas in _context.Marcas on produto.MarcaId equals marcas.MarcaId
            //               join categorias in _context.Categorias on produto.CategoriaId equals categorias.CategoriaId
            //               where produto.ProdutoId == id
            //               select new ProdutoViewModel
            //               {
            //                   Nome = produto.Nome,
            //                   Sku = produto.Sku,
            //                   Descricao = produto.Descricao,
            //                   Preco = produto.Preco,
            //                   Caracteristicas = produto.Caracteristicas,
            //                   DataLancamento = produto.DataLancamento,
            //                   CategoriaId = produto.CategoriaId,
            //                   Categoria = produto.Categoria,
            //                   MarcaId = produto.MarcaId,
            //                   Marca = produto.Marca
            //               };

            //return produtos.SingleOrDefault();
        }

        public int Insert(ProdutoViewModel produtoModel)
        {
            _context.Produtos.Add(produtoModel);
            _context.SaveChanges();
            return produtoModel.ProdutoId;
        }

        public void Update(ProdutoViewModel produtoModel)
        {
            _context.Produtos.Update(produtoModel);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var produto = new ProdutoViewModel()
            {
                ProdutoId = id
            };

            _context.Produtos.Remove(produto);
            _context.SaveChanges();
        }
    }
}
