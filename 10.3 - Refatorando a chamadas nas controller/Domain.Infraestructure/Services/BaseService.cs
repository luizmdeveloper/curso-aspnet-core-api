using Domain.Infraestructure.Notifications;

namespace Domain.Infraestructure.Services
{
    public class BaseService
    {
        private readonly INotification _notification;

        public BaseService(INotification notification)
        {
            this._notification = notification;
        }
    }
}
