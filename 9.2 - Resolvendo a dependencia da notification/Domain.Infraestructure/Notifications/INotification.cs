using System.Collections.Generic;

namespace Domain.Infraestructure.Notifications
{
    public interface INotification
    {
        void AddError(string message);
        bool HasError();
        List<string> Errors();
        void AddErrorFatal(string message);
        bool HasErrorFatal();
        List<string> ErrorsFatal();
    }
}
