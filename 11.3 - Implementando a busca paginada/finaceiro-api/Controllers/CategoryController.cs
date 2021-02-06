using Domain.Infraestructure.Controllers;
using Domain.Infraestructure.Notifications;
using LuizMario.Domain.Core.Entity;
using LuizMario.Domain.Core.Service;
using LuizMario.Dto.Filter;
using LuizMario.Dto.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace finaceiro_api.Controllers
{
    [Route("v1/categories")]
    public class CategoryController : BasicController
    {
        private readonly CategoryService _service;

        public CategoryController(CategoryService service, INotification notification) : base(notification) 
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<ResponsePaginationDto<Category>> Get([Required] [FromQuery] CategoryFilterDto filter)
        {
            return Ok(_service.Search(filter));
        }

        [HttpGet("{categoryId}")]
        public ActionResult<Category> GetById(int categoryId) 
        {
            var category = _service.FindById(categoryId);

            if (category != null) 
            {
                return Ok(category);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<Category> Save(Category category) 
        {
            if (IsModelValid()) 
            {
                _service.Save(category);
            }
            return Created(category.Id, category);
        }

        [HttpPut("{categoryId}")]
        public ActionResult<Category> Update(int categoryId, [FromBody] Category category)
        {
            var categorySave = _service.FindById(categoryId);

            if (categorySave != null)
            {
                categorySave.Name = category.Name;
                _service.Update(categorySave);
                return Ok(categorySave);
            }

            return NotFound();
        }

        [HttpDelete("{categoryId}")]
        public ActionResult Delete(int categoryId)
        {
            var categorySave = _service.FindById(categoryId);

            if (categorySave == null)
            {
                return NotFound();
            }
            else
            {
                // TODO implementar lógico de foreing key
                _service.Delete(categorySave);
            }

            return NoContent();
        }
    }
}
