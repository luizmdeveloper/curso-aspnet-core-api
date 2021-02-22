using Domain.Infraestructure.Controllers;
using Domain.Infraestructure.Notifications;
using LuizMario.Domain.Core.Entity;
using LuizMario.Domain.Core.Service;
using LuizMario.Dto.Filter;
using LuizMario.Dto.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace finaceiro_api.Controllers
{
    [Route("v1/[controller]")]
    public class CategoryController : BasicController
    {
        private readonly CategoryService _service;

        public CategoryController(CategoryService service, INotification notification) : base(notification) 
        {
            this._service = service;
        }

        /// <summary>
        /// Find all categories
        /// </summary>
        /// <param name="Name"></param> 
        /// <param name="Page"></param> 
        /// <param name="Size"></param> 
        /// <response code="200">Returns content then category</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ResponsePaginationDto<Category>> Get([Required] [FromQuery] CategoryFilterDto filter)
        {
            return Ok(_service.Search(filter));
        }

        /// <summary>
        /// Find category by id
        /// </summary>
        /// <param name="categoryId"></param> 
        /// <response code="200">Return if exists category</response>
        /// <response code="404">Return if not exists category</response>
        [HttpGet("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Category> GetById(int categoryId) 
        {
            var category = _service.FindById(categoryId);

            if (category != null) 
            {
                return Ok(category);
            }

            return NotFound();
        }

        /// <summary>
        /// Insert category
        /// </summary>
        /// <response code="201">Return save success</response>
        /// <response code="400">Return attribute invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Category> Save(Category category) 
        {
            if (IsModelValid()) 
            {
                _service.Save(category);
            }
            return Created(category.Id, category);
        }

        /// <summary>
        /// Update category
        /// </summary>
        /// <response code="200">Return update success</response>
        /// <response code="400">Return attribute invalid</response>
        /// <response code="404">Return if not exists category</response>
        [HttpPut("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Update category
        /// </summary>
        /// <response code="204">Return delete success</response>
        /// <response code="404">Return if not exists category</response>        
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int categoryId)
        {
            var categorySave = _service.FindById(categoryId);

            if (categorySave == null)
            {
                return NotFound();
            }

            // TODO implementar lógico de foreing key
            _service.Delete(categorySave);

            return NoContent();
        }
    }
}
