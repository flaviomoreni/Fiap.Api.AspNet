using Fiap.Api.AspNet.Data;
using Fiap.Api.AspNet.Repository;
using Fiap.Api.AspNet.Repository.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO.Compression;
using System.Linq;
using System.Text;


namespace Fiap.Api.AspNet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region Versionamento
            services.AddApiVersioning(options => {
                options.UseApiBehavior = false;
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(3, 0);
                options.ApiVersionReader =
                    ApiVersionReader.Combine(
                        new HeaderApiVersionReader("x-api-version"),
                        new QueryStringApiVersionReader(),
                        new UrlSegmentApiVersionReader());
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
            #endregion


            #region Swagger
            services.AddSwaggerGen();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            #endregion



            #region Autenticacao e Autorizacao
            var key = Encoding.ASCII.GetBytes(Settings.Secret); 
            services.AddAuthentication(x => 
            { 
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
            }
            ).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false; x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                }
            );
            #endregion

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });


            #region Injeção de dependencias
            services.AddScoped<IMarcaRepository, MarcaRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            var connectionString = Configuration.GetConnectionString("databaseUrl");
            services.AddDbContext<DataContext>(
                option => option.UseSqlServer(connectionString)
                                .EnableSensitiveDataLogging()
                                .LogTo(Console.Write));

            #endregion


            #region Compreesão de dados
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] {"application/json" });
            });
            #endregion



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Versionamento
            app.UseApiVersioning();
            #endregion


            #region Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
                }

                c.DocExpansion(DocExpansion.List);
                c.RoutePrefix = string.Empty;
            });
            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseResponseCompression();
        }
    }
}
