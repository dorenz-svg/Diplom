using Diplom.Infrastructure;
using Diplom.Models;
using Diplom.Models.Entities;
using Diplom.Models.Repositories.Abstract;
using Diplom.Models.Repositories.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
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
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
                .Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
                = new DefaultContractResolver());
            services.AddSignalR();
            services.AddCors();
            services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            var builder = services.AddIdentityCore<MyUser>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<DBContext>();
            identityBuilder.AddSignInManager<SignInManager<MyUser>>();
            services.AddTransient<IFriendsRepository, EFFriendsRepository>();
            services.AddTransient<IUsersRepository, EFUsersRepository>();
            services.AddTransient<IDialogRepository, EFDialogRepository>();
            services.AddTransient<IMessageRepository, EFMessageRepository>();
            services.AddTransient<IPostsRepository, EFPostsRepository>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<ISaveImage, SaveImage>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Diplom", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(
                   opt =>
                   {
                       opt.RequireHttpsMetadata = false;
                       opt.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuerSigningKey = true,
                           IssuerSigningKey = key,
                           ValidateLifetime = true,
                           ValidateAudience = false,
                           ValidateIssuer = false,
                       };
                       opt.Events = new JwtBearerEvents
                       {
                           OnMessageReceived = context =>
                           {
                               var accessToken = context.Request.Query["access_token"];

                           // если запрос направлен хабу
                           var path = context.HttpContext.Request.Path;
                               if (!string.IsNullOrEmpty(accessToken) &&
                                   (path.StartsWithSegments("/chat")))
                               {
                               // получаем токен из строки запроса
                               context.Token = accessToken;
                               }
                               return Task.CompletedTask;
                           }
                       };
                   });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Diplom v1"));
            }
            
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(builder => builder.WithOrigins(Configuration.GetConnectionString("Cors")).AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapControllers();
            });
        }
    }
}
