using Domain.Infraestructure.Notifications;
using Domain.Infraestructure.Repository;
using LuizMario.Domain.Core.Entity;
using System.Data;

namespace LuizMario.Domain.Core.Repository
{
    public class AuthorizationRepository : Repository<Authorization>
    {
        public AuthorizationRepository(IDbConnection connection, INotification notification) : base(connection, notification)
        {
        }
    }
}
