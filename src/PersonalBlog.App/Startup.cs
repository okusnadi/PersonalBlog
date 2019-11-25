using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonalBlog.App.Data;
using PersonalBlog.App.Services;

namespace PersonalBlog.App
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
            services.AddRazorPages();
            services.AddServerSideBlazor(options=>
            {
                options.DetailedErrors = true;
                
            });

            services.AddHttpContextAccessor();
            services.AddDbContext<BlogDbContext>();
            services.AddScoped<BlogService>();

            services.AddSAFe(configure =>
            {
                configure.AddEntityFrameworkCore<BlogDbContext>();
            });

            services.Configure<BlogSettings>(instance => Configuration.Bind("BlogSettings", instance));

            services.AddBlazoredLocalStorage();
            services.AddScoped<AuthenticationStateProvider, SignInService>();
            services.AddScoped<IJsonWebTokenService, JsonWebTokenService>();
            services.AddScoped<IAuthService, SignInService>();

            //initialize database
            using (var scope=services.BuildServiceProvider().CreateScope())
            {
                var database= scope.ServiceProvider.GetRequiredService<BlogDbContext>();

                database.Database.EnsureCreated();
            }
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
