using EmployeeAppServices.Defenitions.BlogDummy;
using EmployeeAppServices.Defenitions.BlogDummyUser;
using EmployeeAppServices.Defenitions.BlogPost;
using EmployeeAppServices.Defenitions.BlogUser;
using EmployeeAppServices.Implementations.BlogDummyServices;
using EmployeeAppServices.Implementations.BlogDummyUserService;
using EmployeeAppServices.Implementations.BlogPostService;
using EmployeeAppServices.Implementations.BlogUserService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeAppServices
{
    public static class EmployeeAppServiceModule
    {
        public static IServiceCollection EmployeeAppServiceCollection(this IServiceCollection services, string baseAddress)
        {
            services.AddHttpClient("ApiBaseUrl", client =>
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

            });

            services.AddScoped<IBlogPostService, BlogPostService>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var logger = provider.GetRequiredService<ILogger<BlogPostService>>();
                return new BlogPostService(httpClientFactory.CreateClient("ApiBaseUrl"), logger);
            });

            services.AddScoped<IBlogUserService, BlogUserService>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var logger = provider.GetRequiredService<ILogger<BlogUserService>>();
                return new BlogUserService(httpClientFactory.CreateClient("ApiBaseUrl"), logger);
            });
            return services;
        }

        public static IServiceCollection EmployeeAppDummyServiceCollection (this IServiceCollection services, string dummyBaseAddress, string AppId)
        {
            services.AddHttpClient("DummyApiBaseUrl", client =>
            {
                client.BaseAddress = new Uri(dummyBaseAddress);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("app-id", AppId);
            });

            services.AddScoped<IBlogDummyUserService, BlogDummyUserService>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var logger = provider.GetRequiredService<ILogger<BlogDummyUserService>>();
                return new BlogDummyUserService(httpClientFactory.CreateClient("DummyApiBaseUrl"), logger);
            });

            services.AddScoped<IBlogDummyPostService, BlogDummyUserPostService>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var logger = provider.GetRequiredService<ILogger<BlogDummyUserPostService>>();
                return new BlogDummyUserPostService(httpClientFactory.CreateClient("DummyApiBaseUrl"), logger);
            });
            return services;
        }
    }
}
