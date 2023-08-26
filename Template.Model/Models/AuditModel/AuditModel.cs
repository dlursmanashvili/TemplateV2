using Template.Model.Models.AuditModel.EnumsForAudit;

namespace Template.Model.Models.AuditModel;

public class AuditModel : BaseEntity<Guid>
{
    public string UserName { get; set; }
    public ActionType ActionType { get; set; }
    public ModelType ModelType { get; set; }
    public EntityName Entity { get; set; }
    public DateTime ActionDate { get; set; } = DateTime.Now;
}
