using Template.Model.Models.Templates.CommandModel;

namespace Template.Service.IServices;

public interface IEmailTemplateService
{
    Task<TemplateResponse> CreateTemplate(CreateTemplateRequest createTemplateRequest);
    Task<TemplateResponse> UpdateTemplate(EditTemplateRequest editTemplateRequest);
    Task<bool> DeleteTemplate(DeleteTemplateRequest deleteTemplateRequest);
    Task<IEnumerable<TemplateResponse>?> GetAllTemplates(string UserName);
    Task<TemplateResponse?> GetTemplateById(GetTemplateRequest getTemplateRequest);
    Task<Dictionary<string, string>?> GetKeysFromTextAsync(GetTemplateRequest getTemplateRequest);
    Task<TemplateResponse> GetGenerateTExt(GenerateTextRequest generateTextRequest);
}
