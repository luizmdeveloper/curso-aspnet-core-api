using Domain.Infraestructure.Notifications;
using Domain.Infraestructure.Repository;
using LuizMario.Domain.Core.Entity;
using System.Data;

namespace LuizMario.Domain.Core.Repository
{
    public class FinancialMovimentRepository : Repository<FinancialMoviment>
    {
        public FinancialMovimentRepository(IDbConnection connection, INotification notification) : base(connection, notification)
        {
        }
    }
}
