using Fiap.Api.AspNet.Model;
using Fiap.Api.AspNet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Fiap.Api.AspNet.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DataContext()
        {
        }


        public DbSet<CategoriaModel> Categoria {get; set;}
        public DbSet<MarcaModel> Marca { get; set; }
        public DbSet<ProdutoModel> Produto { get; set; }
        public DbSet<UsuarioModel> Usuario { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*
            if ( optionsBuilder.IsConfigured == false  )
            {
                var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("databaseUrl")); 
                optionsBuilder.EnableSensitiveDataLogging(); 
                optionsBuilder.LogTo(Console.Write); 
            } 
            */
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MarcaModel>().HasData(
                new MarcaModel(1, "Apple"),
                new MarcaModel(2, "Samsung"),
                new MarcaModel(3, "Google"),
                new MarcaModel(4, "Xiaomi")
            );


            modelBuilder.Entity<CategoriaModel>().HasData(
                new CategoriaModel(1, "Smartphone"),
                new CategoriaModel(2, "Televisor"),
                new CategoriaModel(3, "Notebook"),
                new CategoriaModel(4, "Tablet")
            );


            modelBuilder.Entity<ProdutoModel>().HasData(
                new ProdutoModel(1,"iPhone 12","SKUIPH12","Apple iPhone 12",5000, "", DateTime.Now, 1 , 1)
            );


            modelBuilder.Entity<UsuarioModel>().HasData(
                new UsuarioModel(1,"Admin Senior", "123456", "Senior"),
                new UsuarioModel(2,"Admin Pleno", "123456", "Pleno"),
                new UsuarioModel(3,"Admin Junior", "123456", "Junior")
            );



            base.OnModelCreating(modelBuilder);
        }


    }
}
