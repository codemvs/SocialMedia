using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Data;
using SocialMedia.Infraestructure.Filters;
using SocialMedia.Infraestructure.Repositories;
using System;

namespace SocialMedia.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configuracion de profile automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers()
                // Ingorar referencia circular
                .AddNewtonsoftJson(opt =>
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                // Deshabilitar validacion api controller, configurar las opciones de comportamiento de API
                .ConfigureApiBehaviorOptions(opt =>
                {
                    //opt.SuppressModelStateInvalidFilter = true
                });
            //Configuracio BD
            services.AddDbContext<SocialMediaContext>(opt =>
                    opt.UseSqlServer( Configuration.GetConnectionString("SocialMedia") )
                );
            // Injection Dependencies
            services.AddTransient<IPostRepository, PostRepository>();

            // Configuracion Filters, se registra el filtro asincrono de forma global
            services.AddMvc(opt => opt.Filters.Add<ValidationFilter>())
                // Configue fluen validators; se registra por asemblie por que la capa de validacion esta en una capa diferente de la aplicacion 
                .AddFluentValidation(opt => opt.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
