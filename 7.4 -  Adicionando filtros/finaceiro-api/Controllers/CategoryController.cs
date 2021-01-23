using Dapper;
using finaceiro_api.Entity;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
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
        public ActionResult<IEnumerable<Category>> Get([FromQuery] string name)
        {
            var sql = new StringBuilder(@" SELECT * FROM categories ");
            var parameters = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(name)) 
            {
                sql.Append(" WHERE UPPER(name) LIKE UPPER(@name)");
                parameters.Add("name", "%" + name + "%");
            }

            var categories = this._connection.Query<Category>(sql.ToString(), parameters);
            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public ActionResult<Category> GetById(int categoryId) 
        {
            var sql = @" SELECT * FROM categories 
                         WHERE id =  @id ";
            var category = this._connection.Query<Category>(sql, new { id = categoryId }).FirstOrDefault();

            if (category != null) 
            {
                return Ok(category);
            }

            return NotFound();
        }
    }
}
