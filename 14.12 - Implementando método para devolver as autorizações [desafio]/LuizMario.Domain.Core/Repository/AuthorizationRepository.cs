using Dapper;
using Domain.Infraestructure.Notifications;
using Domain.Infraestructure.Repository;
using LuizMario.Domain.Core.Entity;
using LuizMario.Domain.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;

namespace LuizMario.Domain.Core.Repository
{
    public class AuthorizationRepository : Repository<Authorization>
    {
        public AuthorizationRepository(IDbConnection connection, INotification notification) : base(connection, notification)
        {
        }

        public IEnumerable<AuthorityViewModel> FindAllByProfile(int profileId)
        {
            var sql = @" SELECT authorities.id,
                                authorities.role
                         FROM profilesxauthorities
                         INNER JOIN authorities ON authorities.id = profilesxauthorities.idauthorithies
                         WHERE profilesxauthorities.idprofile = @profileId ";
            var parameters = new Dictionary<string, object>();
            parameters.Add("profileId", profileId);
            return _connection.Query<AuthorityViewModel>(sql, parameters);
        }
    }
}
