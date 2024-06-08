using EmployeeAppModels.BlogPost;
using EmployeeAppServices.Defenitions.BlogPost;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EmployeeAppServices.Implementations.BlogPostService
{
    public class BlogPostService : IBlogPostService
    {
        #region Variable
        private readonly HttpClient _httpClient;
        private readonly ILogger<BlogPostService> _logger;
        #endregion

        #region Ctor
        public BlogPostService(IHttpClientFactory httpClientFactory, ILogger<BlogPostService> logger)
        {
            //Creating client of JsonPlaceHolderApi in program.cs
            _httpClient = httpClientFactory.CreateClient("JsonPlaceHolderApi");
            _logger = logger;
        }
        #endregion

        #region Get All Post API
        public async Task<BlogPostModel> GetAllBlogPostAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("posts");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var blogPostList = JsonConvert.DeserializeObject<List<BlogPostList>>(responseBody);
                return new BlogPostModel { BlogPostListView = blogPostList };
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Request error: {Message}", e.Message);
                throw new Exception("There was an error fetching the sunrise and sunset times. Please check your network connection and try again.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unexpected error: {Message}", e.Message);
                throw new Exception("An unexpected error occurred. Please try again later.");
            }
        }
        #endregion

    }
}
