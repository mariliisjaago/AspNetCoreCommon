using DbAccess_Library.Contracts.Db;
using DbAccess_Library.Contracts.Repos;
using DbAccess_Library.Contracts.Strategies;
using DbAccess_Library.Db;
using DbAccess_Library.Repos;
using DbAccess_Library.Strategies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlazorServerWeb
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
            services.AddServerSideBlazor();

            services.AddScoped<IDataAccess, SqlDataAccess>();
            services.AddSingleton(new ConnectionStringData
            {
#if DEBUG
                SqlConnectionName = "Development"
#else
                SqlConnectionName = "Production"
#endif
            });

            services.AddScoped<IFoodsRepo, SqlFoodsRepo>();
            services.AddScoped<IOrdersRepo, SqlOrdersRepo>();

            services.AddScoped<IDeleteOrderStrategy, DeleteOrderStrategy>();
            services.AddScoped<IGetOrderStrategy, GetOrderStrategy>();
            services.AddScoped<IPlaceOrderStrategy, PlaceOrderStrategy>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
