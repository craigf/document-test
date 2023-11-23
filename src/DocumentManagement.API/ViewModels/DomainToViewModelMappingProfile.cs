using AutoMapper;
using DocumentManagement.Domain.Documents.Model;

namespace DocumentManagement.API.ViewModels
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Document, DocumentViewModel>();
        }
    }
}
