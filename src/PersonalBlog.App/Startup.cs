using Blazored.LocalStorage;
using Markdig;
using Markdig.SyntaxHighlighting;
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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PersonalBlog.App.Settings;

namespace PersonalBlog.App
{
    public class Startup
    {
        public Startup(IConfiguration setting)
        {
            Configuration = setting;
        }

       public readonly static MarkdownPipeline Pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().UseSyntaxHighlighting().Build();

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
            services.AddLocalization(options => options.ResourcesPath = "Cultures");
            

            services.AddHttpContextAccessor();
            services.AddDbContext<BlogDbContext>();
            services.AddScoped<BlogService>();
            services.AddSingleton<ISettingManager, JsonSettingManager>();

            services.AddSAFe(configure =>
            {
                configure.AddEntityFrameworkCore<BlogDbContext>();
            });


            services.AddBlazoredLocalStorage();
            services.AddScoped<AuthenticationStateProvider, SignInService>();
            services.AddScoped<IJsonWebTokenService, JsonWebTokenService>();
            services.AddScoped<IAuthService, SignInService>();

            //initialize database
            using (var scope=services.BuildServiceProvider().CreateScope())
            {
                var database= scope.ServiceProvider.GetRequiredService<BlogDbContext>();

                database.Database.Migrate();
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer()
                ;
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
            var culture = app.ApplicationServices.GetRequiredService<ISettingManager>().GetSystemSetting().Culture;

            app.UseRequestLocalization(new  RequestLocalizationOptions
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(culture),
            });

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
