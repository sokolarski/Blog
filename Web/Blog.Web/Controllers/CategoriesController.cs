namespace Blog.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using Blog.Services.Data;
    using Blog.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : Controller
    {
        private ICategoriesService categoriesService;
        private readonly IPostsService postsService;

        public CategoriesController(ICategoriesService categoriesService, IPostsService postsService)
        {
            this.categoriesService = categoriesService;
            this.postsService = postsService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult ByName(string name, int page =1 )
        {
            var itemsPerPage = 10;
            var viewModel =
           this.categoriesService.GetByName<CategoryViewModel>(name);
            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.ForumPosts = this.postsService.GetByCategoryId<PostInCategoryViewModel>(viewModel.Id, itemsPerPage, (page - 1) * itemsPerPage);

            var count = this.postsService.GetCountByCategoryId(viewModel.Id);
            viewModel.PagesCount = (int)Math.Ceiling((double)count / itemsPerPage);
            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }
    }
}
