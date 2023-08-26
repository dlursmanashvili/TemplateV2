using Audit.Service.IServices;
using Template.Infrastructure.HelperClass;
using Template.Infrastructure.Repositories.Interfaces;
using Template.Model.Exceptions;
using Template.Model.Models.AuditModel;
using Template.Model.Models.AuditModel.EnumsForAudit;
using Template.Model.Models.Templates;
using Template.Model.Models.Templates.CommandModel;
using Template.Service.IServices;

namespace Template.Service.Services;

public class EmailTemplateService : IEmailTemplateService
{
    private readonly IEmailTemplateRepository _TemplateRepository;
    private readonly IAuditService _AuditService;
    public EmailTemplateService(IEmailTemplateRepository templateRepository, IAuditService auditService)
    {
        _TemplateRepository = templateRepository;
        _AuditService = auditService;
    }

    public async Task<TemplateResponse> CreateTemplate(CreateTemplateRequest createTemplateRequest)
    {

        var template = new EmailTemplateModel()
        {
            Id = Guid.NewGuid(),
            Text = TextHelper.CheckNewText(createTemplateRequest.text),
            IsDeleted = false,
        };
        if (createTemplateRequest.text == null || createTemplateRequest.text == " ")
        {
            throw new BadRequestException("please imput normal text");
        }
        else
        {
            var result = await _AuditService.CreateAudit(new AuditModel()
            {
                Id = Guid.NewGuid(),
                UserName = createTemplateRequest.UserName,
                ActionType = ActionType.Create,
                ModelType = ModelType.EmailAddTemplate,
                Entity = EntityName.Template,
                ActionDate = DateTime.Now,
            });
            if (result.IsSuccess)
            {
                await _TemplateRepository.AddAsync(template);
                return new TemplateResponse() { Text = template.Text, Id = template.Id };
            }
            else
            {
                throw new BadRequestException($"{result.SuccessMassage}");
            }
        }
    }

    public async Task<bool> DeleteTemplate(DeleteTemplateRequest deleteTemplateRequest)
    {
        var template = await _TemplateRepository.GetByIdAsync(deleteTemplateRequest.Id);
        if (template == null || template.IsDeleted) { throw new NotFoundException("template not found"); }

        template.IsDeleted = true;

        var result = await _AuditService.CreateAudit(new AuditModel()
        {
            Id = Guid.NewGuid(),
            UserName = deleteTemplateRequest.UserName,
            ActionType = ActionType.Delete,
            ModelType = ModelType.EmailDeleteTemplate,
            Entity = EntityName.Template,
            ActionDate = DateTime.Now,
        });
        if (result.IsSuccess)
        {
            await _TemplateRepository.UpdateAsync(template);
            return true;
        }
        else
        {
            throw new BadRequestException($"{result.SuccessMassage}");
        }
    }

    public async Task<IEnumerable<TemplateResponse>?> GetAllTemplates(string UserName)
    {
        var commandresult = await _AuditService.CreateAudit(new AuditModel()
        {
            Id = Guid.NewGuid(),
            UserName = UserName,
            ActionType = ActionType.GetAll,
            ModelType = ModelType.EmailGetTemplate,
            Entity = EntityName.Template,
            ActionDate = DateTime.Now,
        });
        if (commandresult.IsSuccess)
        {
            var result = await _TemplateRepository.LoadAsync();

            if (result.Any())
            {
                return result.Select(x => new TemplateResponse()
                {
                    Text = x.Text,
                    Id = x.Id
                });
            }
            return null;
        }
        else { throw new BadRequestException(); }
    }

    public async Task<TemplateResponse?> GetTemplateById(GetTemplateRequest getTemplateRequest)
    {
        var commandresult = await _AuditService.CreateAudit(new AuditModel()
        {
            Id = Guid.NewGuid(),
            UserName = getTemplateRequest.UserName,
            ActionType = ActionType.GetOne,
            ModelType = ModelType.EmailGetTemplate,
            Entity = EntityName.Template,
            ActionDate = DateTime.Now,
        });
        if (commandresult.IsSuccess)
        {
            var template = await _TemplateRepository.GetByIdAsync(getTemplateRequest.TemplateId) ?? throw new Exception("Template not found");

            return new TemplateResponse() { Id = getTemplateRequest.TemplateId, Text = template.Text };
        }
        else { throw new BadRequestException(); }
    }

    public async Task<TemplateResponse> UpdateTemplate(EditTemplateRequest editTemplateRequest)
    {
        var commandresult = await _AuditService.CreateAudit(new AuditModel()
        {
            Id = Guid.NewGuid(),
            UserName = editTemplateRequest.UserName,
            ActionType = ActionType.Update,
            ModelType = ModelType.EmailEditTemplate,
            Entity = EntityName.Template,
            ActionDate = DateTime.Now,
        });
        if (commandresult.IsSuccess)
        {
            var template = await _TemplateRepository.GetByIdAsync(editTemplateRequest.Id);
            if (template == null || template.IsDeleted) { throw new NotFoundException("template not found"); }

            template.Text = TextHelper.CheckNewText(editTemplateRequest.Text);

            await _TemplateRepository.UpdateAsync(template);

            return new TemplateResponse() { Text = template.Text, Id = template.Id };
        }
        else { throw new BadRequestException(); }
    }

    public async Task<Dictionary<string, string>?> GetKeysFromTextAsync(GetTemplateRequest getTemplateRequest)
    {
        var reuslt = await GetTemplateById(getTemplateRequest);

        var commandresult = await _AuditService.CreateAudit(new AuditModel()
        {
            Id = Guid.NewGuid(),
            UserName = getTemplateRequest.UserName,
            ActionType = ActionType.GetOne,
            ModelType = ModelType.EmailGetTemplateDictionary,
            Entity = EntityName.Template,
            ActionDate = DateTime.Now,
        });

        if (commandresult.IsSuccess)
        {
            return TextHelper.ReturnDictionaryKeysFromText(reuslt.Text);

        }
        else { throw new BadRequestException(); }

    }
    public async Task<TemplateResponse> GetGenerateTExt(GenerateTextRequest generateTextRequest)
    {
        var request = new GetTemplateRequest() { TemplateId = generateTextRequest.TemplateId, UserName = generateTextRequest.UserName };
        var reuslt = await GetTemplateById(request);
        var commandresult = await _AuditService.CreateAudit(new AuditModel()
        {
            Id = Guid.NewGuid(),
            UserName = generateTextRequest.UserName,
            ActionType = ActionType.GetOne,
            ModelType = ModelType.EmailGenerateTemplate,
            Entity = EntityName.Template,
            ActionDate = DateTime.Now,
        });

        if (commandresult.IsSuccess)
        {
            return new TemplateResponse()
            {
                Text = TextHelper.GetGeneratedAndCangedText(reuslt.Text, generateTextRequest.FromText),
                Id = reuslt.Id
            };

        }
        else { throw new BadRequestException(); }
    }


}