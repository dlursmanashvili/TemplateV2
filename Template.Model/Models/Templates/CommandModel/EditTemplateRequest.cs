namespace Template.Model.Models.Templates.CommandModel;

public class EditTemplateRequest
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public string UserName { get; set; }
}
