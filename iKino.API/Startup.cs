using AutoMapper;
using iKino.API.Domain;
using iKino.API.DTO;
using iKino.API.Repositories;
using iKino.API.Repositories.Interfaces;
using iKino.API.Services;
using iKino.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace iKino.API
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
            services.AddMvc();

            services.AddDbContext<CinemaContext>(ConfigureDatabase);
            services.AddScoped<IDatabase<CinemaContext>, Database>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IHashService, HashService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMovieService, MovieService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(ConfigureToken);
        }

        private void ConfigureDatabase(IServiceProvider serviceProvider, DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseInMemoryDatabase(nameof(CinemaContext));
            //dbContextOptionsBuilder.UseSqlServer(Configuration["Database:ConnectionString"]);
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            Mapper.Initialize(ConfigureMapper);
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseAuthentication();
            app.UseMvc();
        }

        private void ConfigureToken(JwtBearerOptions options)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Configuration["JWT:Issuer"],
                ValidateAudience = false,
                ValidAudience = Configuration["JWT:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(Configuration["JWT:Key"])),
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }
        private void ConfigureMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<User, UserDto>();
            cfg.CreateMap<Movie, MovieDto>();
        }
    }
}