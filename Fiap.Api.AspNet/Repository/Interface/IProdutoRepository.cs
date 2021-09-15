using Fiap.Api.AspNet.Models;
using System.Collections.Generic;

namespace Fiap.Api.AspNet.Repository.Interface
{
    public interface IProdutoRepository
    {
        public IList<ProdutoViewModel> FindAll();
        public ProdutoViewModel FindById(int id);
        public int Insert(ProdutoViewModel produtoModel);
        public void Delete(int id);
        public void Update(ProdutoViewModel produtoModel);
    }
}
