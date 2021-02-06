using Domain.Infraestructure.Controllers;
using Domain.Infraestructure.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace finaceiro_api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class PersonController : BasicController
    {
        public PersonController(INotification notification) : base(notification)
        {
        }
    }
}
