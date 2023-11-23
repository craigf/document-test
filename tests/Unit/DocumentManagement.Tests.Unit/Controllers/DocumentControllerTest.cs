using DocumentManagement.Tests.Unit.Helpers;
using DocumentManagement.API.Controllers;
using Xunit;
using MediatR;
using Moq;
using AutoMapper;
using DocumentManagement.API.ViewModels;
using AutoFixture.Xunit2;
using DocumentManagement.Domain.Documents.Command;
using Microsoft.AspNetCore.Mvc;
using DocumentManagement.Domain.Documents.Model;
using FluentAssertions;

namespace DocumentManagement.Tests.Unit.Controllers
{
    [Trait("Document ", "Unit")]
    public class DocumentControllerTest : ControllerTestBase
    {
        private readonly DocumentController _controller;
        private readonly Mock<IMediator> _mediatR;

        public DocumentControllerTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainToViewModelMappingProfile());
            });

            var mapper = mappingConfig.CreateMapper();

            _mediatR = new Mock<IMediator>();
            _controller = new DocumentController(_mediatR.Object, mapper);
        }

        [Theory, AutoData]
        public async Task ValidCreateDocument_Returns_OkResult_WithModel(CreateDocumentCommand command, CancellationToken token)
        {
            var document = new Document(command.Title, command.Content);

            _mediatR.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).Returns(Task.FromResult(document));

            var result = await _controller.CreateDocument(command, token);

            Is<OkObjectResult, DocumentViewModel>(result);
        }

        [Theory, AutoData]
        public async Task GetDocumentWithUnknownId_Returns_NotFoundResult(int id, CancellationToken token)
        {
            _mediatR.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Task.FromResult<Document>(null!));

            var result = await _controller.GetDocumentById(id , token);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
