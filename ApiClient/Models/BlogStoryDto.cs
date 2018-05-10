using System;

namespace ApiClient.Models
{
    public class BlogStoryDto
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DatePublished { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
    }
}