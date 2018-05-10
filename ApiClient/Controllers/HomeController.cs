using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiClient.Models;
using RestSharp;

namespace ApiClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var client = new RestClient("http://recruitment.blog.api.eits.localhost:49893/");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            //var getBlogStoriesRequest = new RestRequest("api/blog-story/get-blog-stories", Method.GET);
            //IRestResponse<List<BlogStory>> autoDeserializeReponse = client.Execute<List<BlogStory>>(getBlogStoriesRequest);
            //var blogStories = autoDeserializeReponse.Data;

            //var getBlogStoryRequest1 = new RestRequest("api/blog-story/get-blog-story/{id}", Method.GET);
            ////add url segment, note that you should include the segment in new RestRequest("api/blog-story/get-blog-story/{id}"
            //getBlogStoryRequest1.AddUrlSegment("id", "3274b792-b517-46b6-f121-08d5b1d7e4fc");
            //IRestResponse<BlogStory> autoDeserializeReponse2 = client.Execute<BlogStory>(getBlogStoryRequest1);
            //var blogStory = autoDeserializeReponse2.Data;

            //var getBlogStoryRequest2 = new RestRequest("api/blog-story/get-blog-story", Method.GET);
            ////Add parameter like query string, note that you should NOT include the segment in new RestRequest("api/blog-story/get-blog-story"
            //getBlogStoryRequest2.AddParameter("id", "3274b792-b517-46b6-f121-08d5b1d7e4fc");
            //IRestResponse<BlogStory> autoDeserializeReponse3 = client.Execute<BlogStory>(getBlogStoryRequest2);
            //var blogStory2 = autoDeserializeReponse3.Data;

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
