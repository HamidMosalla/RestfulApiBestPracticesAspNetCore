using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiClient.Models;
using RestSharp;
using RestSharp.Authenticators;
using AspNetCoreWebApiSamples.Models;

namespace ApiClient.Controllers
{
    public class ApiService
    {
        
    }


    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var client = new RestClient("http://localhost:2500/api");
            //client.Authenticator = new HttpBasicAuthenticator(username, password);

            var getAuthorsRequest = new RestRequest("authors", Method.GET);
            IRestResponse<List<AuthorDto>> getAuthorsResponse = client.Execute<List<AuthorDto>>(getAuthorsRequest);
            var authors = getAuthorsResponse.Data;

            var getAuthorRequest = new RestRequest("authors/{id}", Method.GET);
            //add url segment, note that you should include the segment in new RestRequest("authors/{id}"
            getAuthorRequest.AddUrlSegment("id", "412c3012-d891-4f5e-9613-ff7aa63e6bb3");
            IRestResponse<AuthorDto> getAuthorResponse = client.Execute<AuthorDto>(getAuthorRequest);
            var author = getAuthorResponse.Data;

            var getAuthorRequestWithoutSegment = new RestRequest("api/blog-story/get-blog-story", Method.GET);
            //Add parameter like query string, note that you should NOT include the segment in new RestRequest("authors?id=something" use parameter instead
            getAuthorRequestWithoutSegment.AddParameter("id", "412c3012-d891-4f5e-9613-ff7aa63e6bb3");
            IRestResponse<AuthorDto> getAuthorRequestWithoutSegmentResponse = client.Execute<AuthorDto>(getAuthorRequestWithoutSegment);
            var author2 = getAuthorRequestWithoutSegmentResponse.Data;

            ////// execute the request
            //IRestResponse response = client.Execute(getBlogStoryRequest1);
            //var content = response.Content; // raw content as string

            //// or download and save file to disk
            //client.DownloadData(request2).SaveAs(@"C:\Users\Hamid\Desktop");

            // async with deserialization
            //var asyncHandle = client.ExecuteAsync<BlogStory>(request2, r =>
            //{
            //    var blogStory3 = autoDeserializeReponse3.Data;
            //});

            ////// abort the request on demand
            //asyncHandle.Abort();

            var postBlogStoryRequest = new RestRequest("api/blog-story/create-blog-story", Method.POST);
            var blogStoryDto = new BlogStoryDto
            {
                Name = "something555",
                CreatedDate = DateTime.Now,
                DatePublished = DateTime.Now,
                Title = "somethingvery",
                Slug = "ha-something"
            };

            //Note: It adds to the query string not the body
            //postBlogStoryRequest.AddObject(blogStoryDto);

            //// or just whitelisted properties
            //request.AddObject(blogStoryDto, "PersonId", "Name", ...);

            postBlogStoryRequest.AddJsonBody(blogStoryDto);

            IRestResponse<BlogStoryDto> autoDeserializeReponse = client.Execute<BlogStoryDto>(postBlogStoryRequest);
            var createdBlogStory = autoDeserializeReponse.Data;

            //// easily add HTTP Headers
            //request.AddHeader("header", "value");

            //// add files to upload (works with compatible verbs)
            //request.AddFile("file", path);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
