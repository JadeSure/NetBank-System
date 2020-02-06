using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models.DataManager;

namespace WebApi
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
            // for more shows in limited json file size (32 => 256)
            services.AddControllers().AddJsonOptions(option => { option.JsonSerializerOptions.PropertyNamingPolicy = null; option.JsonSerializerOptions.MaxDepth = 256; });

            services.AddTransient<AccountManager>();
            services.AddTransient<BillPayManager>();
            services.AddTransient<CustomerManager>();
            services.AddTransient<LoginManager>();
            services.AddTransient<PayeeManager>();
            services.AddTransient<TransactionManager>();


            services.AddDbContext<NwbaDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString(nameof(NwbaDbContext)));
                // this is for lazy loading
                // options.UseLazyLoadingProxies();
            });

            
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
