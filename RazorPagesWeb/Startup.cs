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

namespace RazorPagesWeb
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
            services.AddRazorPages();

            services.AddSingleton(new ConnectionStringData
            {
                SqlConnectionName = "Default"
            });

            services.AddSingleton<IDataAccess, SqlDataAccess>();

            services.AddScoped<IFoodsRepo, SqlFoodsRepo>();
            services.AddScoped<IOrdersRepo, SqlOrdersRepo>();

            services.AddScoped<IPlaceOrderStrategy, PlaceOrderStrategy>();
            services.AddScoped<IGetOrderStrategy, GetOrderStrategy>();
            services.AddScoped<IDeleteOrderStrategy, DeleteOrderStrategy>();
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
                endpoints.MapRazorPages();
            });
        }
    }
}
