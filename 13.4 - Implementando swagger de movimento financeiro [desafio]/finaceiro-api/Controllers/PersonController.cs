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
    [ApiController]
    public class PersonController : BasicController
    {
        private readonly PersonService _service;
        public PersonController(PersonService service, INotification notification) : base(notification)
        {
            this._service = service;
        }

        /// <summary>
        /// Find all persons
        /// </summary>
        /// <param name="Name"></param> 
        /// <param name="Page"></param> 
        /// <param name="Size"></param> 
        /// <response code="200">Returns content then person</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ResponsePaginationDto<Person>> Search([Required] [FromQuery] PersonFilterDto filter) 
        {
            return Ok(_service.Search(filter));
        }

        /// <summary>
        /// Find by id 
        /// </summary>
        /// <param name="Id"></param> 
        /// <response code="200">Returns if exists person</response>
        /// <response code="404">Returns if not exists</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Person> FindById(int id) 
        {
            var person = _service.FindById(id);

            if (person != null) 
            {
                return Ok(person);
            }

            return NotFound();
        }

        /// <summary>
        /// Insert person
        /// </summary>
        /// <response code="201">Return save success</response>
        /// <response code="400">Return attribute invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Person> Save(Person person) 
        {
            if (IsModelValid()) 
            {
                _service.Save(person);
            }

            return Created(person.Id, person);
        }

        /// <summary>
        /// Update person
        /// </summary>
        /// <response code="200">Return update success</response>
        /// <response code="400">Return attribute invalid</response>
        /// <response code="404">Return if not exists person</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Person> Update([Required] int id, [FromBody] Person person) 
        {
            var personSave = _service.FindById(id);

            if (IsModelValid()) 
            { 
                if (personSave == null) 
                {
                    return NotFound();
                }

                personSave.Name = person.Name;
                personSave.Created = person.Created;
                personSave.Active = person.Active;

                _service.Update(personSave);
            }

            return CustomizeResponse(personSave);
        }

        /// <summary>
        /// Delete person
        /// </summary>
        /// <response code="204">Return delete success</response>
        /// <response code="404">Return if not exists person</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete([Required] int id) 
        {
            var person = _service.FindById(id);

            if (person == null) 
            {
                return NotFound();
            }

            _service.Delete(person);
            return NoContent();
        }

        /// <summary>
        /// active person
        /// </summary>
        /// <response code="204">Return update success</response>
        /// <response code="404">Return if not exists person</response>
        [HttpPut("{id}/active")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Person> Actived([Required] int id) 
        {
            var person = _service.FindById(id);
            if (person == null)
            {
                return NotFound();
            }
            _service.Actived(person);
            return CustomizeResponse(person);
        }

        /// <summary>
        /// active person
        /// </summary>
        /// <response code="204">Return update success</response>
        /// <response code="404">Return if not exists person</response>
        [HttpPut("{id}/inactive")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Person> Inactive([Required] int id)
        {
            var person = _service.FindById(id);
            if (person == null)
            {
                return NotFound();
            }

            _service.Inactive(person);
            return CustomizeResponse(person);
        }
    }
}
