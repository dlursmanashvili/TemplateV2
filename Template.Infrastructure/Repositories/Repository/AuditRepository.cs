using Audit.Infrastructure.Repositories.Interfaces;
using Template.Infrastructure.DataBaseHelper;
using Template.Infrastructure.Repositories.Repository;
using Template.Model.Models.AuditModel;

namespace Audit.Infrastructure.Repositories.Repository;

public class AuditRepository : RepositoryBase<AuditModel>, IAuditRepository
{
    public AuditRepository(ApplicationDbContext context) : base(context)
    {
    }
}
