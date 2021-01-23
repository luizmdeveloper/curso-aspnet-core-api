using Dapper;
using Dapper.Contrib.Extensions;
using finaceiro_api.Filter;
using finaceiro_api.Pagination;
using LuizMario.Domain.Core.Entity;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;

namespace finaceiro_api.Controllers
{
    [Route("v1/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private IDbConnection _connection;

        public CategoryController() 
        {
            this._connection = new MySqlConnection("Server=localhost;Port=3306;Database=finaceiro;Uid=root;Pwd=root;persistsecurityinfo=True;");
            this._connection.Open();
        }

        [HttpGet]
        public ActionResult<ResponsePagination<Category>> Get([FromQuery] [Required] CategoryFilter filter)
        {
            var sql = new StringBuilder(@" SELECT * FROM categories ");
            var parameters = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(filter.Name)) 
            {
                sql.Append(" WHERE UPPER(name) LIKE UPPER(@name)");
                parameters.Add("name", "%" + filter.Name + "%");
            }

            sql.Append(" LIMIT @page, @size ");
            parameters.Add("page", filter.Page != 0 ? filter.Page * filter.Size : filter.Page);
            parameters.Add("size", filter.Size == 0 ? 5 : filter.Size);

            var categories = this._connection.Query<Category>(sql.ToString(), parameters);
            return Ok(new ResponsePagination<Category>(10, categories));
        }

        [HttpGet("{categoryId}")]
        public ActionResult<Category> GetById(int categoryId) 
        {
            var category = FindById(categoryId);

            if (category != null) 
            {
                return Ok(category);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<Category> Save(Category category) 
        {
            this._connection.Insert<Category>(category);
            return Created($"{Request.Path.Value}/{category.Id}", category);
        }

        [HttpPut("{categoryId}")]
        public ActionResult<Category> Update(int categoryId, [FromBody] Category category) 
        {
            var categorySave = FindById(categoryId);

            if (categorySave != null) 
            {
                categorySave.Name = category.Name;
                this._connection.Update<Category>(categorySave);
                return Ok(categorySave);
            }

            return NotFound();
        }

        [HttpDelete("{categoryId}")]
        public ActionResult Delete(int categoryId)
        {
            var categorySave = FindById(categoryId);

            if (categorySave == null)
            {
                return NotFound();
            }
            else 
            {
                // TODO implementar lógico de foreing key
                this._connection.Delete<Category>(categorySave);
            }

            return NoContent();
        }


        private Category FindById(int categoryId) 
        {
            var sql = @" SELECT * FROM categories 
                         WHERE id =  @id ";

            return this._connection.Query<Category>(sql, new { id = categoryId }).FirstOrDefault();
        }
    }
}
