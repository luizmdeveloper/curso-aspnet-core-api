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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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

        [HttpPut("{id}/active")]
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

        [HttpPut("{id}/inactive")]
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
