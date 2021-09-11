using Fiap.Api.AspNet.Models;
using System.Collections.Generic;

namespace Fiap.Api.AspNet.Repository
{
    public interface IMarcaRepository
    {

        public IList<MarcaModel> FindAll();

        public MarcaModel FindById(int id);

        public int Insert(MarcaModel marcaModel);

        public void Delete(int id);

        public void Update(MarcaModel marcaModel);

    }
}
