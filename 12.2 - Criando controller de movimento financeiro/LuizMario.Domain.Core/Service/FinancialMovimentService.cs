using Domain.Infraestructure.Notifications;
using Domain.Infraestructure.Services;
using LuizMario.Domain.Core.Repository;

namespace LuizMario.Domain.Core.Service
{
    public class FinancialMovimentService : BaseService
    {
        private readonly FinancialMovimentRepository _repository;

        public FinancialMovimentService(FinancialMovimentRepository repository,INotification notification) : base(notification)
        {
            this._repository = repository;
        }
    }
}
