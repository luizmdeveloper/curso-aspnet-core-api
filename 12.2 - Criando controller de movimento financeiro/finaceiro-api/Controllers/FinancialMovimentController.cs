using Domain.Infraestructure.Controllers;
using Domain.Infraestructure.Notifications;
using LuizMario.Domain.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace finaceiro_api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class FinancialMovimentController : BasicController
    {
        private readonly FinancialMovimentService _service;
        public FinancialMovimentController(FinancialMovimentService service, INotification notification) : base(notification)
        {
            this._service = service;
        }

        [HttpGet]
        public string HelloWorld() 
        {
            return "Olá, mundo!";
        }
    }
}
