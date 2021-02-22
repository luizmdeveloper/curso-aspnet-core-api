using Domain.Infraestructure.Notifications;
using Domain.Infraestructure.Services;
using LuizMario.Domain.Core.Entity;
using LuizMario.Domain.Core.Repository;
using LuizMario.Dto.Input;

namespace LuizMario.Domain.Core.Service
{
    public class UserService : BaseService
    {
        private readonly UserRepository _repository;
        public UserService(UserRepository repository, INotification notification) : base(notification)
        {
            this._repository = repository;
        }

        public User FindById(int id) 
        {
            return _repository.FindById(id);
        }

        public User FindByEmailAndPassword(UserInputDto userInput) 
        {
            return _repository.FindByEmailAndPassword(userInput);
        }
    }
}
