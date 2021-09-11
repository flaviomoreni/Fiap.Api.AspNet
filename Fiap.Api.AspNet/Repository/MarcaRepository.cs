using Fiap.Api.AspNet.Data;
using Fiap.Api.AspNet.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Fiap.Api.AspNet.Repository
{
    public class MarcaRepository : IMarcaRepository
    {

        private readonly DataContext context;
 
        public MarcaRepository(DataContext _context)
        {
            this.context = _context;
        }

        public IList<MarcaModel> FindAll()
        {
            return context.Marca.AsNoTracking().ToList();
        }

        public MarcaModel FindById(int id)
        {
            return context.Marca.AsNoTracking().FirstOrDefault( m => m.MarcaId == id) ;
        }

        public int Insert(MarcaModel marcaModel)
        {
            context.Marca.Add(marcaModel);
            context.SaveChanges();
            return marcaModel.MarcaId;
        }

        public void Update(MarcaModel marcaModel)
        {
            context.Marca.Update(marcaModel);
            context.SaveChanges();
        }


        public void Delete(int id)
        {
            MarcaModel m = new MarcaModel()
            {
                MarcaId = id
            };

            context.Marca.Remove(m);
            context.SaveChanges();
        }
    }
}
