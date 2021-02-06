using System.Collections.Generic;
using System.Linq;

namespace Domain.Infraestructure.Notifications
{
    public class Notification : INotification
    {
        private List<string> _errors;
        private List<string> _fatalErrors;

        public Notification() 
        {
            this._errors = new List<string>();
            this._fatalErrors = new List<string>();
        }

        public void AddError(string message)
        {
            _errors.Add(message);
        }
        public List<string> Errors()
        {
            return _errors;
        }

        public bool HasError()
        {
            return _errors.Any();
        }

        public void AddFatalErro(string message)
        {
            _fatalErrors.Add(message);
        }

        public void AddErrorFatal(string message)
        {
            _fatalErrors.Add(message);
        }

        public bool HasErrorFatal()
        {
            return _fatalErrors.Any();
        }

        public List<string> ErrorsFatal()
        {
            return _fatalErrors;
        }
    }
}
