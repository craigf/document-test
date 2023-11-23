using DocumentManagement.Domain.Documents.Command;
using DocumentManagement.Domain.Documents.Validator;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace DocumentManagement.Tests.Unit.Domain.Validators
{
    [Trait("Document", "Unit")]
    public class CreateDocumentValidationTests
    {
        private readonly CreateDocumentValidator _validator;

        public CreateDocumentValidationTests()
        {
            _validator = new CreateDocumentValidator();
        }

        [Fact]
        internal void Should_Have_Error_When_Title_Is_Empty()
        {
            var result = _validator.TestValidate(new CreateDocumentCommand(string.Empty, "Test"));
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        internal void Should_Have_Error_When_Title_Is_Null()
        {
            var result = _validator.TestValidate(new CreateDocumentCommand(null, "test"));
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        internal void Should_Have_Error_When_Content_Is_Empty()
        {
            var result = _validator.TestValidate(new CreateDocumentCommand("test", string.Empty));
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        internal void Should_Have_Error_When_Content_Is_Null()
        {
            var result = _validator.TestValidate(new CreateDocumentCommand("test", null));
            result.IsValid.Should().BeFalse();
        }
    }
}
