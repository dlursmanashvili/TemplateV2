using Template.Model;
using Template.Model.Models.AuditModel;

namespace Audit.Service.IServices;

public interface IAuditService
{
    Task<CommandResult> CreateAudit(AuditModel createAuditRequest);
    Task<CommandResult> UpdateAudit(AuditModel editAuditRequest);
    Task<bool> DeleteAudit(AuditModel deleteAuditRequest);
    Task<IEnumerable<CommandResult>?> GetAllAudits();
    Task<CommandResult> GetAuditById(Guid id);
}
