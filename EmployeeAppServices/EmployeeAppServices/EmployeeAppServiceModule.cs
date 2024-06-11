using EmployeeAppServices.Defenitions.BlogPost;
using EmployeeAppServices.Implementations.BlogPostService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAppServices
{
    public static class EmployeeAppServiceModule
    {
        public static IServiceCollection EmployeeAppServiceCollection (this IServiceCollection services , string baseAddress)
        {
            services.AddHttpClient("JsonPlaceHolderApi", client =>
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddScoped<IBlogPostService, BlogPostService>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var logger = provider.GetRequiredService<ILogger<BlogPostService>>();
                return new BlogPostService(httpClientFactory.CreateClient("JsonPlaceHolderApi"), logger);
            });
            return services;
        }
    }
}
