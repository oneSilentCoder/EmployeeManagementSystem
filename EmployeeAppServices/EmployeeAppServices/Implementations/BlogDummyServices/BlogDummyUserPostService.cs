using EmployeeAppModels.BlogDummy;
using EmployeeAppModels.BlogDummyUser;
using EmployeeAppServices.Defenitions.BlogDummy;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EmployeeAppServices.Implementations.BlogDummyServices
{
    public class BlogDummyUserPostService: IBlogDummyPostService
    {
        #region Variable Declaration
        private readonly HttpClient _httpClient;
        private readonly ILogger<BlogDummyUserPostService> _logger;
        #endregion

        #region ctor
        public BlogDummyUserPostService(HttpClient httpClient, ILogger<BlogDummyUserPostService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        #endregion

        #region Get User Post Api
        public async Task<BlogDummyUserPostModel> GetBlogDummyUserPost(BlogDummyUserPostRequest ObjBlogDummyUserPostRequest)
        {
            try
            {
                Uri BaseUri;
                var builder = new UriBuilder();
                if (ObjBlogDummyUserPostRequest.UserId != null)
                {
                    BaseUri = new Uri(_httpClient.BaseAddress!, $"user/{ObjBlogDummyUserPostRequest.UserId}/post");
                    builder = new UriBuilder(BaseUri)
                    {
                        //The UriBuilder class includes the port by default when the scheme is HTTPS. If you don't want to include the port number in the URL, you can explicitly set the port to -1 in the UriBuilder.
                        Port = -1
                    };
                }
                else
                {
                    BaseUri = new Uri(_httpClient.BaseAddress!, $"post");
                    builder = new UriBuilder(BaseUri)
                    {
                        //The UriBuilder class includes the port by default when the scheme is HTTPS. If you don't want to include the port number in the URL, you can explicitly set the port to -1 in the UriBuilder.
                        Port = -1
                    };
                }              
               
                var query = HttpUtility.ParseQueryString(builder.Query);
                query["limit"] = ObjBlogDummyUserPostRequest.Limit.ToString();
                query["page"] = ObjBlogDummyUserPostRequest.Page.ToString();
                builder.Query = query.ToString();

                HttpResponseMessage response = await _httpClient.GetAsync(builder.ToString());
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                //Exclamation mark is used for managing null
                BlogDummyUserPostModel ObjBlogDummyUserPostModel = JsonConvert.DeserializeObject<BlogDummyUserPostModel>(responseBody)!;

                //Fetching Comments of the post fetched using the post id
                //foreach (var UserPostList in ObjBlogDummyUserPostModel.data)
                //{
                //    string UserPostId = UserPostList.id;
                //    BaseUri = new Uri(_httpClient.BaseAddress!, $"post/"+ UserPostId + "/comment");
                //    builder = new UriBuilder(BaseUri)
                //    {
                //        Port = -1
                //    };
                //    var CommentQuery = HttpUtility.ParseQueryString(builder.Query);
                //    builder.Query = CommentQuery.ToString();
                //    HttpResponseMessage CommentResponse = await _httpClient.GetAsync(builder.ToString());
                //    response.EnsureSuccessStatusCode();
                //    string CommentResponseBody = await CommentResponse.Content.ReadAsStringAsync();
                //    // Use a temporary wrapper to deserialize the comments
                //    var tempCommentWrapper = JsonConvert.DeserializeObject<BlogDummyCommentListView>(CommentResponseBody);
                //    UserPostList.data = tempCommentWrapper; // Assign the comments to the Data property

                //    //ObjBlogDummyUserPostModel.data = JsonConvert.DeserializeObject<BlogDummyCommentListView[]>(CommentResponseBody)!;
                //}              

                return ObjBlogDummyUserPostModel;

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
