﻿namespace Blog.Service.DTOs
{
    public class ArticleResponse
    {
        public string Name { get; set; } = null!;
        public string Content { get; set; } = null!;
        public Guid UserID { get; set; }
    }
}