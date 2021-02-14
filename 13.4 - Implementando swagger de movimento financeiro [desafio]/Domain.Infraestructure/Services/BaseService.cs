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

        protected void AddError(string message)
        {
            _notification.AddError(message);
        }

        protected bool HasError()
        {
            return _notification.HasError();
        }

        protected bool IsValid() 
        {
            return !HasError();
        }
    }
}
