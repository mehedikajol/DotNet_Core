using System;

namespace Blog.Web.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public string ImageName { get; set; }
        public DateTime? CreatedTime { get; set; } = DateTime.Now;
    }
}
