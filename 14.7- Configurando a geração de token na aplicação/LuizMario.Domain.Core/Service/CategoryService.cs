using Domain.Infraestructure.Notifications;
using Domain.Infraestructure.Services;
using LuizMario.Domain.Core.Entity;
using LuizMario.Domain.Core.Repository;
using LuizMario.Dto.Filter;
using LuizMario.Dto.Pagination;

namespace LuizMario.Domain.Core.Service
{
    public class CategoryService : BaseService
    {
        private readonly CategoryRepository _repository;
        public CategoryService(CategoryRepository repository, INotification notification) : base(notification)
        {
            this._repository = repository;
        }

        public ResponsePaginationDto<Category> Search(CategoryFilterDto filter)
        {
            return _repository.Search(filter);
        }

        public Category FindById(int categoryId)
        {
            return _repository.FindById(categoryId);
        }

        public void Save(Category category)
        {
            _repository.Save(category);
        }

        public void Update(Category category)
        {
            _repository.Update(category);
        }

        public void Delete(Category category)
        {
            _repository.Delete(category);
        }
    }
}
