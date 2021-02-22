using Domain.Infraestructure.Notifications;
using Domain.Infraestructure.Services;
using LuizMario.Domain.Core.Repository;

namespace LuizMario.Domain.Core.Service
{
    public class AuthorizationService : BaseService
    {
        private readonly AuthorizationRepository _repository;

        public AuthorizationService(INotification notification, AuthorizationRepository repository) : base(notification)
        {
            this._repository = repository;
        }
    }
}
