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
    [Route("v1/[controller]")]
    [ApiController]
    public class PersonController : BasicController
    {
        private readonly PersonService _service;
        public PersonController(PersonService service, INotification notification) : base(notification)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<ResponsePaginationDto<Person>> Search([Required] [FromQuery] PersonFilterDto filter) 
        {
            return Ok(_service.Search(filter));
        }

        [HttpGet("{id}")]
        public ActionResult<Person> FindById(int id) 
        {
            var person = _service.FindById(id);

            if (person != null) 
            {
                return Ok(person);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<Person> Save(Person person) 
        {
            if (IsModelValid()) 
            {
                _service.Save(person);
            }

            return Created(person.Id, person);
        }
    }
}
