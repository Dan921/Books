using Application.Interfaces;
using Application.Logic;
using Data.Context;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWebAPI
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
            services.AddDbContext<LibraryContext>(item => item.UseSqlServer(Configuration.GetConnectionString("myconn")));

            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IAuthorsRepository, AuthorsRepository>();
            services.AddScoped<ISeriesRepository, SeriesRepository>();
            services.AddScoped<IBookCoverRepository, BookCoverRepository>();
            services.AddScoped<IGenresRepository, GenresRepository>();
            services.AddScoped<ITagsRepository, TagsRepository>();

            services.AddScoped<IBooksQueriesService, BooksQueriesService>();
            services.AddScoped<IAuthorsQueriesService, AuthorsQueriesService>();
            services.AddScoped<IBookSeriesQueriesService, BookSeriesQueriesService>();
            services.AddScoped<IBookGenresQueriesService, BookGenresQueriesService>();
            services.AddScoped<IBookTagsQueriesService, BookTagsQueriesService>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BooksWebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BooksWebAPI v1"));
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
