namespace Blog.Web.Controllers
{
    using System.Diagnostics;
    using Blog.Data.Models;
    using Blog.Services.Data;
    using Blog.Web.ViewModels;
    using Blog.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ICategoriesService catService;

        public HomeController(ICategoriesService catService)
        {
            this.catService = catService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel()
            {
                Categories = this.catService.GetAll<IndexCategoryViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
