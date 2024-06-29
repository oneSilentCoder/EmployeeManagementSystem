using EmployeeAppModels.BlogDummyUser;
using EmployeeAppServices.Defenitions.BlogDummyUser;
using EmployeeAppServices.Defenitions.BlogUser;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAppServices.Implementations.BlogDummyUserService
{
    public class BlogDummyUserService : IBlogDummyUserService
    {
        #region Variable Declaration
        private readonly HttpClient _httpClient;
        private readonly ILogger<BlogDummyUserService> _logger;
        #endregion

        #region Ctor
        public BlogDummyUserService (HttpClient httpClient , ILogger<BlogDummyUserService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        #endregion

        #region Get User Api
        public async Task<BlogDummyUserModel> GetAllBlogUserAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("user");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                BlogDummyUserModel? ObjBlogDummyUserModel = JsonConvert.DeserializeObject<BlogDummyUserModel>(responseBody)!;
                return ObjBlogDummyUserModel;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Request error: {Message}", e.Message);
                throw new Exception("There was an error fetching data. Please check your network connection and try again.");
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
