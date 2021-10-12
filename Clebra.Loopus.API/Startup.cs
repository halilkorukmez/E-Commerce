using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clebra.Loopus.DataAccess;
using Clebra.Loopus.Service;
using Clebra.Loopus.Service.AboutDataService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Clebra.Loopus.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddScoped<IProductDataService , ProductDataService>();
            services.AddScoped<IProductCategoryDataService, ProductCategoryDataService >();
            services.AddScoped<IUserDataService, UserDataService>();
            services.AddScoped<IColorDataService, ColorDataService>();
            services.AddScoped<IClothTypeDataService, ClothTypeDataService>();
            services.AddScoped<IDiscountDefinitionDataService, DiscountDefinitionDataService>();
            services.AddScoped<ICommentDataService, CommentDataService>();
            services.AddScoped<IOrderDataService, OrderDataService>();
            services.AddScoped<IOrderLineDataService, OrderLineDataService>();
            services.AddScoped<IAddressDataService, AddressDataService>();
            services.AddScoped<IProductFileDataService, ProductFileDataService>();
            services.AddScoped<IProductStockDataService, ProductStockDataService>();
            services.AddScoped<IProductSizeDataService, ProductSizeDataService>();
            services.AddScoped<IYarnTypeDataService, YarnTypeDataService>();
            services.AddScoped<ICountryDataService, CountryDataService>();
            services.AddScoped<ICityDataService, CityDataService>();
            services.AddScoped<IDistrictDataService, DistrictDataService>(); 
            services.AddScoped<INeighborhoodDataService, NeighborhoodDataService>();
            services.AddScoped<IBigTextDataService, BigTextDataService >();
            services.AddScoped<ISmallTextDataService, SmallTextDataService>();
            services.AddScoped<ISubImageDataService, SubImageDataService>();
            services.AddScoped<ISliderDataService, SliderDataService>();
            services.AddScoped<IAboutService, AboutDataService>();

            services.AddDbContextPool<LoopusDataContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("LoopusConnectionString"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Clebra.Loopus.API", Version = "v1"});
            });


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var clockSkew = Configuration["LoopusToken:ClockSkew"];

                    var issuers = new List<string>()
                        {
                          Configuration["LoopusToken:Issuer"]
                        };
                    var audiences = new List<string>()
                        {
                          Configuration["LoopusToken:Audiences"]
                        };


                    SecurityKey accessTokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["LoopusToken:AccessTokenKey"]));


                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuers = issuers,
                        ValidateAudience = true,
                        ValidAudiences = audiences,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKeys = new List<SecurityKey>() { accessTokenKey },
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(clockSkew))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = ctx => 
                            Task.CompletedTask,
                        OnTokenValidated = ctx => 
                            Task.CompletedTask,
                        OnAuthenticationFailed = ctx => 
                            Task.CompletedTask
                    };
                });

            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clebra.Loopus.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}