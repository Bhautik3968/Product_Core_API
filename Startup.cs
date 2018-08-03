using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductCoreAPI.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProductCoreAPI.Helpers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json.Serialization;
namespace ProductCoreAPI
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(Options =>
            {                
                Options.RequireHttpsMetadata = false;
                Options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer="ProductCoreAPI.Bearer",
                    ValidAudience="ProductCoreAPI.Bearer",
                    IssuerSigningKey=JwtSecurityKey.Create("ProductCoreAPI-secret-key")
                    
                };
            }); 

            services.AddAuthorization(options =>
            {
                options.AddPolicy("User",
                    policy => policy.RequireClaim("UserId"));
            });          
            services.AddCors();
            services.AddMvc(setupAction =>
            {               
                setupAction.ReturnHttpNotAcceptable = true;
                /* setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()); */
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(options =>options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            var connectionString = @"Data Source=T4BhautikPC;Initial Catalog=Test;Persist Security Info=True;User ID=sa; Password=He@lth25;Max Pool Size=3000;Pooling=True;";
            services.AddDbContext<ProductContext>(x => x.UseSqlServer(connectionString));
            services.AddDbContext<UserContext>(x => x.UseSqlServer(connectionString));
            services.AddDbContext<DbErrorContext>(x => x.UseSqlServer(connectionString));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature != null)
                        {                                           
                            var errMessage = exceptionHandlerFeature.Error.Message;
                        }
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Please Contact your Administrator.");
                    });
                });
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();              
            app.UseCors(builder => builder
                       .AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials()
                      );
            app.UseMvc();
        }
    }
}
