using Microsoft.AspNetCore.Mvc;
using Template.Model.Models.Templates.CommandModel;
using Template.Service.IServices;

namespace Template.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailTemplateController : ControllerBase
    {
        private readonly IEmailTemplateService _templateService;

        public EmailTemplateController(IEmailTemplateService templateService)
        {
            _templateService = templateService;
        }

        /// <summary>
        /// Create Template text.
        /// </summary>
        /// <param name="createTemplateRequest"></param>
        [HttpPost]
        [ProducesResponseType(typeof(TemplateResponse), 201)] // abrunebs 500 gasasworebelia
        public async Task<IActionResult> Create(CreateTemplateRequest createTemplateRequest)
        {
            var result = await _templateService.CreateTemplate(createTemplateRequest);
            return new JsonResult(result);
        }

        /// <summary>     
        /// Updates info about Template text.
        /// </summary>
        /// <param name="editTemplateRequest"></param>        
        [HttpPut]
        [ProducesResponseType(typeof(TemplateResponse), 200)]
        public async Task<IActionResult> Update(EditTemplateRequest editTemplateRequest)
            => Ok(await _templateService.UpdateTemplate(editTemplateRequest));


        /// <summary>
        /// Gets current Template text by given Id.
        /// </summary>
        /// <param name="getTemplateRequest"></param>
        [HttpGet]
        [Route("GetTemplateById")]
        [ProducesResponseType(typeof(TemplateResponse), 200)]
        public async Task<IActionResult> GetById([FromQuery]GetTemplateRequest getTemplateRequest)
          => Ok(await _templateService.GetTemplateById(getTemplateRequest));

        /// <summary>
        /// Gets current Template keys for values by given Id.
        /// </summary>
        /// <param name="getTemplateRequest"></param>
        [HttpGet]
        [Route("GetKeysFromTextAsync")]
        [ProducesResponseType(typeof(TemplateResponse), 200)]
        public async Task<IActionResult> GetDictionaryFromText([FromQuery] GetTemplateRequest getTemplateRequest)
          => Ok(await _templateService.GetKeysFromTextAsync(getTemplateRequest));

        /// <summary>
        ///Generate Text.
        /// </summary>
        /// <param name="generateTextRequest"></param>
        [HttpPost]
        [Route("GetGenerateText")]
        [ProducesResponseType(typeof(TemplateResponse), 200)]
        public async Task<IActionResult> GetGenerateText([FromBody] GenerateTextRequest generateTextRequest) 
          => Ok(await _templateService.GetGenerateTExt(generateTextRequest));

        /// <summary>
        /// Gets list of Template text.
        /// </summary>
        /// <param name="UserName"></param>
        [HttpGet]
        [Route("GetAllTemplates/{UserName}")]
        [ProducesResponseType(typeof(TemplateResponse), 200)]
        public async Task<IActionResult> GetAll(string UserName)
            => Ok(await _templateService.GetAllTemplates(UserName));

        /// <summary>
        /// Deletes Template text.
        /// </summary>
        /// <param name="deleteTemplateRequest"></param>
        [HttpDelete]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(DeleteTemplateRequest deleteTemplateRequest)
        {
            await _templateService.DeleteTemplate(deleteTemplateRequest);
            return NoContent();
        }
    }
}

