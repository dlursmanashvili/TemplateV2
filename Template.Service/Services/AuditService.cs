using Audit.Infrastructure.Repositories.Interfaces;
using Audit.Service.IServices;
using Microsoft.IdentityModel.Tokens;
using Template.Model;
using Template.Model.Models.AuditModel;

namespace Template.Service.Services;

public class AuditService : IAuditService
{
    private readonly IAuditRepository _auditrepository;

    public AuditService(IAuditRepository auditrepository)
    {
        _auditrepository = auditrepository;
    }

    public async Task<CommandResult> CreateAudit(AuditModel createAuditRequest)
    {
        if (createAuditRequest.UserName.IsNullOrEmpty())
            return new CommandResult() { SuccessMassage = "Username is null or empty", IsSuccess = false };

        await _auditrepository.AddAsync(createAuditRequest);
        return new CommandResult();
    }

    public Task<bool> DeleteAudit(AuditModel deleteAuditRequest)
    {
        throw new System.NotImplementedException();
    }

    public Task<IEnumerable<CommandResult>?> GetAllAudits()
    {
        throw new System.NotImplementedException();
    }

    public Task<CommandResult> GetAuditById(Guid id)
    {
        throw new System.NotImplementedException();
    }

    public Task<CommandResult> UpdateAudit(AuditModel editAuditRequest)
    {
        throw new System.NotImplementedException();
    }
}