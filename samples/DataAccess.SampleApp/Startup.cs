using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Digipolis.DataAccess;
using Microsoft.Extensions.Options;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;

namespace DataAccess.SampleApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var configPath = Path.Combine(env.ContentRootPath, "_config");

            var builder = new ConfigurationBuilder()
                .SetBasePath(configPath)
                .AddJsonFile("dataaccess.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Configure DataAccess service

            //var connection = @"Server=localhost;Port=5432;Database=data_access_sample_app;User Id=postgres;Password=root;";
            var connection = BuildConnectionString(services);

            services.AddDbContext<SampleAppContext>(options => options.UseNpgsql(connection.ToString()));

            services.AddDataAccess<SampleAppContext>();

            // Add framework services.
            services.AddMvc();
        }

        private ConnectionString BuildConnectionString(IServiceCollection services)
        {
            var section = Configuration.GetSection("ConnectionString");
            services.Configure<ConnectionString>(section);

            var configureOptions = services.BuildServiceProvider().GetRequiredService<IConfigureOptions<ConnectionString>>();
            var connectionString = new ConnectionString();
            configureOptions.Configure(connectionString);
            return connectionString;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
