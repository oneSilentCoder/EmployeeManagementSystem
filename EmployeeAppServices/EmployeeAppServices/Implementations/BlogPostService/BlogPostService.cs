using EmployeeAppModels.BlogPost;
using EmployeeAppServices.Defenitions.BlogPost;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace EmployeeAppServices.Implementations.BlogPostService
{
    public class BlogPostService : IBlogPostService
    {
        #region Variable
        private readonly HttpClient _httpClient;
        private readonly ILogger<BlogPostService> _logger;
        #endregion

        #region Ctor
        public BlogPostService(HttpClient httpClient, ILogger<BlogPostService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        #endregion

        #region Get All Post API
        public async Task<BlogPostModel> GetAllBlogPostAsync(BlogPostList ObjBlogPostList)
        {
            try
            {
                var Uri = _httpClient.BaseAddress;
                if (Uri!=null)
                {
                    HttpResponseMessage response = await _httpClient.GetAsync("posts/?userId="+ ObjBlogPostList.userId);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var blogPostList = JsonConvert.DeserializeObject<List<BlogPostList>>(responseBody);
                    return new BlogPostModel { BlogPostListView = blogPostList }; 
                }
                else
                {
                    return new BlogPostModel { BlogPostListView = null };
                }
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Request error: {Message} " + e.Message);
                throw new Exception("There was an error fetching the sunrise and sunset times. Please check your network connection and try again.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Main catch error: {Message} " + e.Message);
                throw new Exception("An unexpected error occurred. Please try again later.");
            }
        }
        #endregion

    }
}
