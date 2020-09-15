namespace Blog.Web.ViewModels.Posts
{
    using Blog.Data.Models;
    using Blog.Services.Mapping;

    public class CategoryDropDownViewModel : IMapFrom<Category>
    {

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
