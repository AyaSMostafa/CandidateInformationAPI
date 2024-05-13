using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CandidateInformationAPI.Data;
using CandidateInformationAPI.Repositories;
using CandidateInformationAPI.Services;
using AutoMapper;
using Microsoft.OpenApi.Models;
using CandidateInformationAPI.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using CandidateInformationAPI.Models;
using Microsoft.OpenApi.Any;



namespace CandidateInformationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<ProgramStartup>();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    // Add AutoMapper with explicit assembly reference
                    services.AddAutoMapper(options =>
                    {
                        options.AddMaps(typeof(ProgramStartup).Assembly);
                    });

                    // Add services
                    services.AddControllers();

                    // Add DbContext
                    services.AddDbContext<CandidateDbContext>(options =>
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));

                    // Add repositories and services
                    services.AddScoped<ICandidateRepository, CandidateRepository>();
                    services.AddScoped<ICandidateService, CandidateService>();
                });
    }

    public class ProgramStartup
    {
        public ProgramStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
      

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Candidate Information API",
                    Version = "v1",
                    Description = "API for storing information about job candidates",
                    Contact = new OpenApiContact
                    {
                        Name = "Aya Bayoumi",
                        Email = "ayasmostafa01@gmail.com",
                        Url = new Uri("https://linkedin.com/in/aya-saeed-mostafa"),
                    }
                });
                c.MapType<string>(() => new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString("")
                });


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey

                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            });

            services.AddControllers();

            // Add DbContext
            services.AddDbContext<CandidateDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Add repositories and services
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<ICandidateService, CandidateService>();

            // Add AutoMapper
            // services.AddAutoMapper(typeof(ProgramStartup).Assembly);
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<CandidateDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudiance"],
                    IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
                };
            });

            services.AddCors(corsOptions => {
                corsOptions.AddPolicy("luftbornPolicy", corsPolicyBuilder =>
                {
                    corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseHttpsRedirection();

            app.UseCors("CandidateInformationAPIPolicy");

            app.UseRouting();

            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Candidate Information API V1");
            });
        }
    }
}
