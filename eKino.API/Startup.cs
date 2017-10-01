using AutoMapper;
using eKino.Core.Domain;
using eKino.Core.Repository;
using eKino.Infrastructure.Database;
using eKino.Infrastructure.DTO;
using eKino.Infrastructure.Repositories;
using eKino.Infrastructure.Services;
using eKino.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Text;

namespace eKino.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // MVC
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            });

            // settings
            var databaseConfiguration = Configuration.GetSection("Database");
            if (!databaseConfiguration.Exists())
                throw new Exception("Settins not found.");
            services.Configure<DatabaseSettings>(databaseConfiguration);

            var tokenConfiguration = Configuration.GetSection("Token");
            if (!tokenConfiguration.Exists())
                throw new Exception("Settins not found.");
            services.Configure<TokenSettings>(tokenConfiguration);


            var hashConfiguration = Configuration.GetSection("Hash");
            if (!hashConfiguration.Exists())
                throw new Exception("Settins not found.");
            services.Configure<HashSettings>(hashConfiguration);


            // automapper config
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Movie, MovieDto>();
                cfg.CreateMap<User, UserDto>();
            });

            // singletons
            services.AddSingleton(config.CreateMapper());
            services.AddSingleton<IHashService, HashService>();
            services.AddSingleton<ITokenService, TokenService>();

            // repositories
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // services
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IUserService, UserService>();

            // auth
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var tokenSettings = Configuration.GetSection("Token").Get<TokenSettings>();
                var symmetricKey = Encoding.UTF8.GetBytes(tokenSettings.Key);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey),
                    ValidAudience = tokenSettings.Audience,
                    ValidIssuer = tokenSettings.Issuer,

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });

            // database
            services.AddDbContext<CinemaContext>(options =>
            {
                var databaseSettings = Configuration.GetSection("Database").Get<DatabaseSettings>();
                if (databaseSettings.InMemory)
                    options.UseInMemoryDatabase(nameof(CinemaContext));
                else if (string.IsNullOrWhiteSpace(databaseSettings.ConnectionString))
                    throw new Exception("Connection string was not found.");
                else
                    options.UseSqlServer(databaseSettings.ConnectionString);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CinemaContext context, IUserService userService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            context.Seed(userService);
            app.UseAuthentication();


            app.UseMvc();
        }
    }
}