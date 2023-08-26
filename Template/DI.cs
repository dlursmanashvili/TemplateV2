using Audit.Infrastructure.Repositories.Interfaces;
using Audit.Infrastructure.Repositories.Repository;
using Audit.Service.IServices;
using Template.Infrastructure.Repositories.Interfaces;
using Template.Infrastructure.Repositories.Repository;
using Template.Service.IServices;
using Template.Service.Services;

namespace Template.Api;

public static class DI
{
    public static void DependecyResolver(this IServiceCollection services)
    {
        services.AddScoped<ISmsTemplateService, SmsTemplateService>();
        services.AddScoped<IEmailTemplateService, EmailTemplateService>();
        services.AddScoped<IAuditService, AuditService>();

        services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
        services.AddScoped<ISmsTemplateReporistory, SmsTemplateReporistory>();
        services.AddScoped<IAuditRepository, AuditRepository>();
    }
}
