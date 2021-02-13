using Domain.Infraestructure.Controllers;
using Domain.Infraestructure.Notifications;
using LuizMario.Domain.Core.Entity;
using LuizMario.Domain.Core.Service;
using LuizMario.Dto.Filter;
using LuizMario.Dto.Pagination;
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
        public ActionResult<ResponsePaginationDto<FinancialMoviment>> Search([FromQuery] FinancialMovimentFilterDto filter) 
        {
            return Ok(_service.Search(filter));
        }

        [HttpGet("{id}")]
        public ActionResult<FinancialMoviment> FindById(int id) 
        {
            var financialMoviment = _service.FindById(id);

            if (financialMoviment == null) 
            {
                return NotFound();
            }

            return Ok(financialMoviment);
        }

        [HttpPost]
        public ActionResult<FinancialMoviment> Save(FinancialMoviment financialMoviment) 
        {
            if (IsModelValid()) 
            {
                var response = _service.Save(financialMoviment);
                return Created(response.Id, response);
            }

            return CustomizeResponse();
        }

    }
}
