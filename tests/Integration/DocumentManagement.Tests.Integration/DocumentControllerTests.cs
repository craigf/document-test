using DocumentManagement.Tests.Integration.Helpers;
using System.Net;
using System.Net.Http.Json;
using DocumentManagement.Domain.Documents.Model;
using FluentAssertions;
using Xunit;

namespace DocumentManagement.Tests.Integration
{
    [Trait("Document", "Integration")]
    public  class DocumentControllerTests : ControllerTestBase
    {
        private const string Endpoint = "/api/v1/document";

        public DocumentControllerTests(ClaimInjectorApplicationFactory factory)
            : base(factory)
        {

        }

        [Fact]
        public async Task Should_Delete_WhenDocument_Exists()
        {
            var document = new Document("Title", "Content");

            var response = await Client.PostAsJsonAsync(Endpoint, document);

            var newDocumentResponse = await response.Content.ReadFromJsonAsync<Document>();

            var httpResponse = await Client.DeleteAsync($"{Endpoint}/{newDocumentResponse.Id}");
            
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Return_ErrorOnCreating_When_Title_Missing()
        {
            var document = new Document(string.Empty, "Content");

            var httpResponse = await Client.PostAsJsonAsync(Endpoint, document);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_Return_NotFoundResponse_When_Invalid_Id_Passed()
        {
            var document = new Document(string.Empty, "Content");

            var httpResponse = await Client.GetAsync($"{Endpoint}/1");

            httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
