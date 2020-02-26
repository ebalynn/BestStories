using Balynn.BestStories.Api.EndPoints;
using Balynn.BestStories.Api.Models;
using Balynn.BestStories.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Balynn.BestStories.Api
{
    public class Startup
    {
        private const string ApplicationSettingsSection = "ApplicationSettings";
        public const string ResponseCacheProfileName = "Default";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var applicationSettings = Configuration.GetSection(ApplicationSettingsSection).Get<AppSettingsModel>();

            services.AddMvc(options =>
            {
                options.CacheProfiles.Add(ResponseCacheProfileName, new CacheProfile
                {
                    Duration = applicationSettings.ResponseCacheDurationSeconds
                });
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddLogging();
            services.AddMemoryCache();

            services.AddSingleton<ICacheSettings>(applicationSettings);
            services.AddSingleton<IStoriesApiSettings>(applicationSettings);

            services.AddSingleton<IStoriesCachingService, StoriesCachingService>();
            services.AddSingleton<IStoriesEndPoint, StoriesEndPoint>();
            services.AddSingleton<ICachedStoriesEndPointDecorator, CachedStoriesEndPointDecorator>();
        }

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
