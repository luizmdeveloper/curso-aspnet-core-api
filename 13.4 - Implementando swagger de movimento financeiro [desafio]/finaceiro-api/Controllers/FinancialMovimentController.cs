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

        /// <summary>
        /// Find all moviment financial
        /// </summary>
        /// <param name="From"></param> 
        /// <param name="To"></param> 
        /// <param name="Category"></param> 
        /// <param name="Person"></param> 
        /// <param name="Page"></param> 
        /// <param name="Size"></param> 
        /// <response code="200">Returns content then moviment financial</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ResponsePaginationDto<FinancialMoviment>> Search([FromQuery] FinancialMovimentFilterDto filter) 
        {
            return Ok(_service.Search(filter));
        }

        /// <summary>
        /// Find by id 
        /// </summary>
        /// <param name="Id"></param> 
        /// <response code="200">Returns if exists moviment financial</response>
        /// <response code="404">Returns if not exists</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<FinancialMoviment> FindById(int id) 
        {
            var financialMoviment = _service.FindById(id);

            if (financialMoviment == null) 
            {
                return NotFound();
            }

            return Ok(financialMoviment);
        }

        /// <summary>
        /// Insert financial moviment
        /// </summary>
        /// <response code="201">Return save success</response>
        /// <response code="400">Return attribute invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<FinancialMoviment> Save(FinancialMoviment financialMoviment) 
        {
            if (IsModelValid()) 
            {
                var response = _service.Save(financialMoviment);
                return Created(response.Id, response);
            }

            return CustomizeResponse();
        }

        /// <summary>
        /// Update moviment financial
        /// </summary>
        /// <response code="200">Return update success</response>
        /// <response code="400">Return attribute invalid</response>
        /// <response code="404">Return if not exists moviment financial</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<FinancialMoviment> Update(int Id, FinancialMoviment financialMoviment)
        {
            var financialMovimentSave = _service.FindById(Id);

            if (financialMoviment == null) 
            {
                return NotFound();
            }

            if (IsModelValid())
            {
                financialMovimentSave.Type = financialMoviment.Type;
                financialMovimentSave.DateCreated = financialMoviment.DateCreated;
                financialMovimentSave.DatePaid = financialMoviment.DatePaid;
                financialMovimentSave.CategoryId = financialMoviment.CategoryId;
                financialMovimentSave.PersonId = financialMoviment.PersonId;
                financialMovimentSave.Value = financialMoviment.Value;
                financialMovimentSave.Observation = financialMoviment.Observation;
                _service.Update(financialMovimentSave);
            }

            return CustomizeResponse(financialMovimentSave);
        }

        /// <summary>
        /// Delete moviment financial
        /// </summary>
        /// <response code="204">Return delete success</response>
        /// <response code="404">Return if not exists moviment financial</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int Id) 
        {
            var finacialMoviment = _service.FindById(Id);

            if (finacialMoviment == null) 
            {
                return NotFound();
            }

            _service.Delete(finacialMoviment);
            return NoContent();
        }
    }
}
