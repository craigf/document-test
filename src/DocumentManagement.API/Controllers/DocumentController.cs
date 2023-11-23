using Microsoft.AspNetCore.Mvc;
using System.Net;
using AutoMapper;
using DocumentManagement.Domain.Documents.Command;
using Swashbuckle.AspNetCore.Annotations;
using MediatR;
using DocumentManagement.API.ViewModels;
using DocumentManagement.Domain.Documents.Query;

namespace DocumentManagement.API.Controllers
{
    [ApiVersion("1.0")]
    public sealed class DocumentController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DocumentController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [SwaggerOperation(summary: "Create Document")]
        [HttpPost]
        [ProducesResponseType(typeof(DocumentViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateDocument([FromBody] CreateDocumentCommand command, CancellationToken token)
        {
            var response = await _mediator.Send(command, token);

            return Ok(_mapper.Map<DocumentViewModel>(response));
        }

        [SwaggerOperation(summary: "Get Document By Id", OperationId = "GetDocumentById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(DocumentViewModel), StatusCodes.Status200OK)]
        [HttpGet("{id:int}", Name = "Get Document By Id")]
        public async Task<IActionResult> GetDocumentById(int id, CancellationToken token)
        {
            var response = await _mediator.Send(new GetDocumentByIdQuery(id), token);
            
            if (response is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DocumentViewModel>(response));
        }

        [SwaggerOperation(summary: "Delete Document", OperationId = "DeleteDocument")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id:int}", Name = "Delete Document")]
        public async Task<IActionResult> DeleteDocumentById(int id, CancellationToken token)
        {
            await _mediator.Send(new DeleteDocumentCommand(id), token);

            return Ok();
        }

        [SwaggerOperation(summary: "Update Document", OperationId = "Update")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(DocumentViewModel), StatusCodes.Status200OK)]
        [HttpPut(Name = "Update Document")]
        public async Task<IActionResult> UpdateSiteMapping([FromBody] UpdateDocumentCommand command, CancellationToken token)
        {
            var response = await _mediator.Send(command, token);
            
            if (response is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DocumentViewModel>(response));
        }
    }
}
