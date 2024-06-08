using EmployeeAppModels;
using EmployeeAppServices.Defenitions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeAppServices.Implementations
{
    public class BlogPostService : IBlogPostService
    {        
        #region Variable
        public readonly HttpClient _httpClient;
        #endregion

        #region Ctor
        public BlogPostService(IHttpClientFactory httpClientFactory)
        {
            //Creating client of JsonPlaceHolderApi in program.cs
            _httpClient = httpClientFactory.CreateClient("JsonPlaceHolderApi");
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
            catch (Exception Ex)
            {
                throw;
            }
        }
        #endregion

    }
}
