using Domain.Infraestructure.Notifications;
using Domain.Infraestructure.Services;
using LuizMario.Domain.Core.Repository;

namespace LuizMario.Domain.Core.Service
{
    public class ProfileService : BaseService
    {
        private readonly ProfileRepository _repository;

        public ProfileService(ProfileRepository repository, INotification notification) : base(notification)
        {
            this._repository = repository;
        }
    }
}
