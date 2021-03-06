using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Mem
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
            services.AddSingleton<UserService>();
            services.AddSingleton<JWTTokenService>();
            services.AddSingleton<NoteService>();
            services.AddSingleton<SecurityService>();

            services
                .AddAuthentication(DefaultJwtScheme)
                .AddJwtBearer(JwtBearerConfig);

            services.AddRouting();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Offset Virtual Path root of the reverse proxy
            app.UsePathBase("/" + Configuration["App:AppRoot"]);
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "/Error",
                    defaults: new { controller = "Home", action = "Error"});

                // Catch all Route - catches anything not caught be other routes
                routes.MapRoute(
                    name: "catch-all",
                    template: "{*url}",
                    defaults: new { controller = "Home", action = "Index" }
                );
            });



        }

        private static void DefaultJwtScheme(Microsoft.AspNetCore.Authentication.AuthenticationOptions opt)
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }

        private void JwtBearerConfig(JwtBearerOptions opt)
        {
            opt.TokenValidationParameters = JWTTokenService.StandardValidationParameters(Configuration);
        }
    }
}
