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

namespace AspNetAPI
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
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("WideOpen", builder => builder.AllowAnyOrigin()
                                                                .AllowAnyMethod()
                                                                .AllowAnyHeader());
            });

            services.AddSingleton(new ConnectionStringData
            {
#if DEBUG
                SqlConnectionName = "Development"
#else
                SqlConnectionName = "Production"
#endif
            });
            services.AddScoped<IDataAccess, SqlDataAccess>();

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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("WideOpen");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
