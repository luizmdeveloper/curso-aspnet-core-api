﻿using Domain.Infraestructure.Controllers;
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
        private readonly CategoryRepository _repository;

        public CategoryController(CategoryRepository repository, INotification notification) : base(notification) 
        {
            this._repository = repository;
        }

        [HttpGet]
        public ActionResult<ResponsePaginationDto<Category>> Get([Required] [FromQuery] CategoryFilterDto filter)
        {
            return Ok(_repository.Search(filter));
        }

        [HttpGet("{categoryId}")]
        public ActionResult<Category> GetById(int categoryId) 
        {
            var category = _repository.FindById(categoryId);

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
                _repository.Save(category);
            }
            return Created(category.Id, category);
        }

        [HttpPut("{categoryId}")]
        public ActionResult<Category> Update(int categoryId, [FromBody] Category category)
        {
            var categorySave = _repository.FindById(categoryId);

            if (categorySave != null)
            {
                categorySave.Name = category.Name;
                _repository.Update(categorySave);
                return Ok(categorySave);
            }

            return NotFound();
        }

        [HttpDelete("{categoryId}")]
        public ActionResult Delete(int categoryId)
        {
            var categorySave = _repository.FindById(categoryId);

            if (categorySave == null)
            {
                return NotFound();
            }
            else
            {
                // TODO implementar lógico de foreing key
                _repository.Delete(categorySave);
            }

            return NoContent();
        }
    }
}
