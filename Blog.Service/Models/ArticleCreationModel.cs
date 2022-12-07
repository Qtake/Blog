namespace Blog.Service.Models
{
    public class ArticleCreationModel
    {
        public string Name { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string[] TagLine { get; set; } = null!;
    }
}
