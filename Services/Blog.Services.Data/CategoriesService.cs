namespace Blog.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Blog.Data.Common.Repositories;
    using Blog.Data.Models;
    using Blog.Services.Mapping;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> catRepo;

        public CategoriesService(IDeletableEntityRepository<Category> catRepo)
        {
            this.catRepo = catRepo;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query = this.catRepo.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        {
            var category = this.catRepo.All()
                .Where(x => x.Name.Replace(" ", "-") == name.Replace(" ", "-"))
                .To<T>().FirstOrDefault();
            return category;
        }
    }
}
