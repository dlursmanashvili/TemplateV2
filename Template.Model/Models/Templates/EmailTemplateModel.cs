namespace Template.Model.Models.Templates;

public class EmailTemplateModel : BaseEntity<Guid>
{
    public string Text { get; set; }
}
