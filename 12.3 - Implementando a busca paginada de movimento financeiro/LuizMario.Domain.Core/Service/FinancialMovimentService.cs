using Domain.Infraestructure.Notifications;
using Domain.Infraestructure.Services;
using LuizMario.Domain.Core.Entity;
using LuizMario.Domain.Core.Repository;
using LuizMario.Dto.Filter;
using LuizMario.Dto.Pagination;
using System;

namespace LuizMario.Domain.Core.Service
{
    public class FinancialMovimentService : BaseService
    {
        private readonly FinancialMovimentRepository _repository;

        public FinancialMovimentService(FinancialMovimentRepository repository,INotification notification) : base(notification)
        {
            this._repository = repository;
        }

        public ResponsePaginationDto<FinancialMoviment> Search(FinancialMovimentFilterDto filter)
        {
            return new ResponsePaginationDto<FinancialMoviment>(_repository.CalculeteTotalElements(filter), _repository.Find(filter));
        }
    }
}
