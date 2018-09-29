using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Options;
using TestForWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TestForWebAPI
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
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(MyFilter2));
            });
            services.AddMvc();
            services.AddDbContext<PersonContext>(opt => opt.UseInMemoryDatabase("PersonList"));
            services.Configure<AppOptions>(Configuration);
            services.Configure<AppOptions2>(Configuration.GetSection("Test"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
            loggerFactory.AddNLog();
            LogManager.LoadConfiguration("NLog.config");

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute("default", "{controller=Person}/{action=Get}/{FirestName?}");
            //});
        }
    }
}
