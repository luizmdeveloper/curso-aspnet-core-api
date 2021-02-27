using Domain.Infraestructure.Notifications;
using Domain.Infraestructure.Services;
using LuizMario.Domain.Core.Entity;
using LuizMario.Domain.Core.Repository;
using LuizMario.Domain.Core.ViewModel;
using System.Collections.Generic;

namespace LuizMario.Domain.Core.Service
{
    public class AuthorizationService : BaseService
    {
        private readonly AuthorizationRepository _repository;

        public AuthorizationService(INotification notification, AuthorizationRepository repository) : base(notification)
        {
            this._repository = repository;
        }

        public IEnumerable<AuthorityViewModel> FindAllByProfile(int profileId)
        {
            return this._repository.FindAllByProfile(profileId);
        }
    }
}
