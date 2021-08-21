using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SoftPrimes.BLL.AuthenticationServices;
using SoftPrimes.BLL.BaseObjects;
using SoftPrimes.BLL.Contexts;
using SoftPrimes.Service.IServices;
using SoftPrimes.Service.Services;
using SoftPrimes.Shared.Domains;
using System.IO.Compression;
using System.Text;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using HelperServices.Hubs;
using Microsoft.AspNetCore.Diagnostics;
using HelperServices;
using NSwag.CodeGeneration.TypeScript;
using NSwag;
using System.IO;
using System.Net.Http;
using System.Net;
using IHelperServices;
using IHelperServices.Models;
using Microsoft.AspNetCore.Http.Features;

namespace SoftPrimes.UI
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
            services.Configure<BearerTokensOptions>(options => Configuration.GetSection("BearerTokens").Bind(options));
            services.Configure<AppSettings>(options => Configuration.Bind(options));
            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = true;
            });
            //Enable GZip Compression
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                // You Can use fastest compression
                options.Level = CompressionLevel.Optimal;
            });
            // ApplicationDbContext.connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddHttpContextAccessor();
            services.AddOptions();
            //services.Configure<IHelperServices.Models.AppSettings>(options => Configuration.Bind(options));
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                          //.AllowCredentials();
                      });
            });
            //services.AddCors();
            // Using Cache with ServerState option
            services.AddDistributedMemoryCache();
            // Default IdleTimeout 20 Minutes
            services.AddSession(opt =>
            {
                opt.Cookie.IsEssential = true;
            });
            //var context = services.BuildServiceProvider()
            //         .GetService<MasarContext>();
            //  UnitOfWork<ApplicationDbContext> _unitOfWork = services.BuildServiceProvider().GetService<UnitOfWork<ApplicationDbContext>>();
            services.AddAutoMapper();

            services.AddSwaggerGen(action =>
            {

                action.MapType<FileContentResult>(() => new Microsoft.OpenApi.Models.OpenApiSchema { Type = "file" });
                action.AddSecurityDefinition("Bearer",
                 new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                 {
                     Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                     Name = "Authorization",
                     In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                     Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                     Scheme = "Bearer"
                 });
                action.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                   {
                      {
                          new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                          {
                              Reference = new Microsoft.OpenApi.Models.OpenApiReference
                              {
                                  Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                          Array.Empty<string>()
                      }
                  });
                action.MapType<object>(() => new Microsoft.OpenApi.Models.OpenApiSchema { Type = "any" });
                action.MapType<JToken>(() => new Microsoft.OpenApi.Models.OpenApiSchema { Type = "any" });
                action.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Soft Primes WebApi", Version = "v1" });
                action.EnableAnnotations();

            });
            services.AddSwaggerGenNewtonsoftSupport(); // explicit opt-in
            services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies(true)
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), serverDbContextOptionsBuilder =>
                {
                    int minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
                    serverDbContextOptionsBuilder.CommandTimeout(minutes);
                    serverDbContextOptionsBuilder.EnableRetryOnFailure();
                });
            });
            services.AddSwaggerGenNewtonsoftSupport(); // explicit opt-in
            services.AddUnitOfWork<ApplicationDbContext>();
            services.AddDataProtection().SetApplicationName("Masar4"); //Add this

            services
               .AddAuthentication(options =>
               {
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               })
               .AddJwtBearer(cfg =>
               {
                   cfg.RequireHttpsMetadata = false;
                   cfg.SaveToken = true;
                   cfg.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidIssuer = Configuration["BearerTokens:Issuer"], // site that makes the token
                       ValidateIssuer = false, // TODO: change this to avoid forwarding attacks
                       ValidAudience = Configuration["BearerTokens:Audience"], // site that consumes the token
                       ValidateAudience = false, // TODO: change this to avoid forwarding attacks
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["BearerTokens:Key"])),
                       ValidateIssuerSigningKey = true, // verify signature to avoid tampering
                       ValidateLifetime = true, // validate the expiration
                       ClockSkew = TimeSpan.FromMinutes(5) // tolerance for the expiration date
                   };
                   cfg.Events = new JwtBearerEvents
                   {
                       OnAuthenticationFailed = context =>
                       {
                           ILogger logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                           logger.LogError("Authentication failed.", context.Exception);
                           return Task.CompletedTask;
                       },
                       OnTokenValidated = context =>
                       {
                           //  ITokenValidatorService tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidatorService>();
                           // return tokenValidatorService.ValidateAsync(context);
                           return Task.CompletedTask;
                       },
                       OnChallenge = context =>
                       {
                           ILogger logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                           logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);
                           return Task.CompletedTask;
                       },
                       OnMessageReceived = context =>
                       {
                           Microsoft.Extensions.Primitives.StringValues accessToken = context.Request.Query["access_token"];

                           // If the request is for our hub...
                           PathString path = context.HttpContext.Request.Path;
                           if (!string.IsNullOrEmpty(accessToken) &&
                               (path.StartsWithSegments("/api/SignalR")))
                           {
                               // Read the token out of the query string
                               context.Token = accessToken;
                           }
                           return Task.CompletedTask;
                       }
                   };
               });
            services.AddScoped(typeof(IStringLocalizer), typeof(DbStringLocalizer));
            services.AddHelperServices();
            services.AddBusinessServices();
            services.AddScoped<ITokenStoreService, TokenStoreService>();
            services.AddScoped<IDataProtectService, DataProtectService>();
            services.AddScoped<IMailServices, MailServices>();
            services.AddControllers()
                //if Swagger comment next line
                .AddMvcOptions(options => options.Filters.Add(new AuthorizeFilter()))
                .AddNewtonsoftJson(o =>
                {
                    o.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                    o.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
            services.AddSignalR(options => options.EnableDetailedErrors = true);
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot";
            });
            // To use Username instead of ConnectionId in Communication 
            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            services.Configure<FormOptions>(options =>

            {

                options.ValueLengthLimit = int.MaxValue;

                options.MultipartBodyLengthLimit = int.MaxValue;

                options.MultipartHeadersLengthLimit = int.MaxValue;

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("AllowAllHeaders");
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    IExceptionHandlerFeature error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;
                    if (error != null && error.Error is SecurityTokenExpiredException)
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            State = 401,
                            Msg = "Token Expired"
                        }));
                    }
                    else if (error != null && error.Error != null)
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            State = 500,
                            Msg = error.Error.Message
                        }));
                    }
                    else
                    {
                        await next();
                    }
                });
            });
            app.UseResponseCompression();
            app.UseSession();
            app.UseSwagger(action =>
            {

            });
            app.UseSwaggerUI(action => { action.SwaggerEndpoint("/swagger/v1/swagger.json", "Masar WebApi"); });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
            if (env.IsDevelopment())
            {
                Task.Run(() =>
                {
                    using (var handler = new HttpClientHandler() { Credentials = CredentialCache.DefaultCredentials })
                    {
                        using (HttpClient httpClient = new HttpClient(handler))
                        {
                            string SourceDocumentAbsoluteUrl = Configuration["SwaggerToTypeScriptClientGeneratorSettings:SourceDocumentAbsoluteUrl"];
                            string OutputDocumentRelativePath = Configuration["SwaggerToTypeScriptClientGeneratorSettings:OutputDocumentRelativePath"];
                            using (Stream contentStream = httpClient.GetStreamAsync(SourceDocumentAbsoluteUrl).Result)
                            using (StreamReader streamReader = new StreamReader(contentStream))
                            {
                                string json = streamReader.ReadToEnd();
                                //NSwag.OpenApiDocument document = NSwag.OpenApiDocument.FromJsonAsync(json).Result;
                                //NSwag.CodeGeneration.TypeScript.TypeScriptClientGeneratorSettings settings = new NSwag.CodeGeneration.TypeScript.TypeScriptClientGeneratorSettings
                                var document = SwaggerDocument.FromJsonAsync(json).Result;
                                var settings = new NSwag.CodeGeneration.TypeScript.SwaggerToTypeScriptClientGeneratorSettings
                                {
                                    OperationNameGenerator = new SwaggerClientOperationNameGenerator(),
                                    ClassName = "SwaggerClient",
                                    Template = TypeScriptTemplate.Angular,
                                    RxJsVersion = 6.0M,
                                    HttpClass = HttpClass.HttpClient,
                                    InjectionTokenType = InjectionTokenType.InjectionToken,
                                    BaseUrlTokenName = "API_BASE_URL",
                                    WrapDtoExceptions = true,
                                };

                                //TypeScriptClientGenerator generator = new TypeScriptClientGenerator(document, settings);
                                var generator = new SwaggerToTypeScriptClientGenerator(document, settings);
                                string code = generator.GenerateFile();
                                new FileInfo(OutputDocumentRelativePath).Directory.Create();
                                try
                                {
                                    File.WriteAllText(OutputDocumentRelativePath, code);
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                        }
                    }
                });

            }
        }
    }
}
