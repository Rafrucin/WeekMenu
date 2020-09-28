using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeekMenu.Client.Areas.Identity;
using WeekMenu.Client.Data;
using WeekMenu.Client.Services;
using Tewr.Blazor.FileReader;
using Microsoft.AspNetCore.Http.Connections;
using WeekMenu.Client.Components;
using WeekMenu.Client.HelperClasses;

namespace WeekMenu.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AuthDbContex>(options =>
                options.UseSqlite(Configuration.GetConnectionString("SqliteConnectionAuth")));

            services.AddDefaultIdentity<IdentityUser>(options => 
                options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AuthDbContex>();

            services.AddDbContext<ModelsDbContext>(options =>
            options.UseSqlite(Configuration.GetConnectionString("SqliteConnectionModels")));

            services.AddTransient<IRecipeService, RecipeService>();

            services.AddTransient<IDayMenuService, DayMenuService>();

            services.AddTransient<Weekhelper>();
            services.AddTransient<DayMenuSetter>();

            services.AddFileReaderService();

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            
            services.AddSignalR(e => {
                e.MaximumReceiveMessageSize = 102400000;
                //e.StreamBufferCapacity = 102;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
