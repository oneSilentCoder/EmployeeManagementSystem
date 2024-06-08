using EmployeeAppModels.BlogUser;
using EmployeeAppServices.Defenitions.BlogUser;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EmployeeAppServices.Implementations.BlogUserService
{
    public class BlogUserService : IBlogUserService
    {
        #region Variable
        public readonly HttpClient _httpClient;
        private readonly ILogger<BlogUserService> _logger;
        #endregion

        #region Ctor
        public BlogUserService(IHttpClientFactory httpClientFactory, ILogger<BlogUserService> logger)
        {
            //Creating client of JsonPlaceHolderApi in program.cs
            _httpClient = httpClientFactory.CreateClient("JsonPlaceHolderApi");
            _logger = logger;
        }
        #endregion

        #region Get User Api
        public async Task<BlogUserModel> GetAllBlogUserAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("users");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var blogUserList = JsonConvert.DeserializeObject<List<BlogUserList>>(responseBody);
                return new BlogUserModel { BlogUserListView = blogUserList };
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
