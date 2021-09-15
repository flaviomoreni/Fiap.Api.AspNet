using Fiap.Api.AspNet.Models;
using System.Collections.Generic;

namespace Fiap.Api.AspNet.Repository.Interface
{
    public interface ICategoriaRepository
    {
        public IList<CategoriaModel> FindAll();
        public CategoriaModel FindById(int id);
        public int Insert(CategoriaModel categoriaModel);
        public void Delete(int id);
        public void Update(CategoriaModel categoriaModel);
    }
}
