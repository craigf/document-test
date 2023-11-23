using DocumentManagement.Domain.Documents.Command;
using FluentValidation;

namespace DocumentManagement.Domain.Documents.Validator
{
    public class CreateDocumentValidator : AbstractValidator<CreateDocumentCommand>
    {
        public CreateDocumentValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Content).NotEmpty();
        }
    }
}
