namespace Template.Model.Models.Templates;

public class SmsTemplateModel : BaseEntity<Guid>
{
    public string Text { get; set; }
}
