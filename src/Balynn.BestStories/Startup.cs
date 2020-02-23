using Balynn.BestStories.EndPoints;
using Balynn.BestStories.Models;
using Balynn.BestStories.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Balynn.BestStories
{
    public class Startup
    {
        private const string ApplicationSettingsSection = "ApplicationSettings";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddLogging();
            services.AddMemoryCache();

            var applicationSettings = Configuration.GetSection(ApplicationSettingsSection).Get<AppSettingsModel>();

            services.AddSingleton<ICacheSettings>(applicationSettings);
            services.AddSingleton<IStoriesApiSettings>(applicationSettings);

            services.AddSingleton<IStoriesCachingService, StoriesCachingService>();
            services.AddSingleton<IStoriesEndPoint, StoriesEndPoint>();
            services.AddSingleton<ICachedStoriesEndPointDecorator, CachedStoriesEndPointDecorator>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
