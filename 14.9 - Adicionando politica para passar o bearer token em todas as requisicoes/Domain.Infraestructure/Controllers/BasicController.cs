using Domain.Infraestructure.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Infraestructure.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [ApiController]
    public class BasicController : ControllerBase
    {
        private readonly INotification _notification;

        public BasicController(INotification notification)
        {
            this._notification = notification;
        }

        protected void AddError(string message) 
        {
            _notification.AddError(message);
        }

        protected bool HasError() 
        {
            return _notification.HasError();
        }

        protected List<string> ListAllErrors() 
        {
            return _notification.Errors();
        }

        protected bool IsModelValid() 
        {
            if (!ModelState.IsValid) 
            {
                foreach (var erro in ModelState.Values.SelectMany(v => v.Errors).Where(z => z.ErrorMessage != "").Select(e => e.ErrorMessage))
                {
                    AddError(erro);
                }
            }

            return IsValid();
        }

        protected ActionResult Created(int id, object response) 
        {
            if (HasError()) 
            {
                return BadRequest(new { errors = ListAllErrors() });
            }

            return Created($"{Request.Path.Value}/{id}", response);
        }

        protected ActionResult CustomizeResponse(object response = null)
        {
            if (HasError())
            {
                return BadRequest(new { errors = ListAllErrors() });
            }

            return Ok(response);
        }

        protected ActionResult NoContent() 
        {
            if (HasError())
            {
                return BadRequest(new { errors = ListAllErrors() });
            }

            return NoContent();
        }

        private bool IsValid()
        {
            return !HasError();
        }
    }
}
