using Domain.Infraestructure.Notifications;
using Domain.Infraestructure.Services;

namespace LuizMario.Domain.Core.Service
{
    public class PersonService : BaseService
    {
        public PersonService(INotification notification) : base(notification)
        {
        }
    }
}
