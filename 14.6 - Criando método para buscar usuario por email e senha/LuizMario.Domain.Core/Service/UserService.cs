using Domain.Infraestructure.Notifications;
using Domain.Infraestructure.Services;
using LuizMario.Domain.Core.Repository;

namespace LuizMario.Domain.Core.Service
{
    public class UserService : BaseService
    {
        private readonly UserRepository _repository;
        public UserService(UserRepository repository, INotification notification) : base(notification)
        {
            this._repository = repository;
        }
    }
}
