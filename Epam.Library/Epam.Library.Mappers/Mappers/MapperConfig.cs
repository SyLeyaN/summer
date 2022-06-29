using AutoMapper;
using Epam.Library.Core.Extensions;
using Epam.Library.Entities;
using Epam.Library.ViewModels.Create;
using Epam.Library.ViewModels.Display;

namespace Epam.Library.Mappers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Book, DisplayBookVM>()
                .ForMember(dst => dst.Information, src => src.MapFrom(s => s.DisplayBookInformationInTable()));
            CreateMap<Patent, DisplayPatentVM>()
                .ForMember(dst => dst.Information, src => src.MapFrom(s => s.DisplayPatentInformationInTable()));
            CreateMap<Newspaper, DisplayNewspaperVM>()
               .ForMember(dst => dst.Information, src => src.MapFrom(s => s.DisplayNewspaperInformationInTable()));
            CreateMap<CreateBookVM, Book>();
            CreateMap<CreateNewspaperVM, Newspaper>();
            CreateMap<CreateNewspaperIssueVM, NewspaperIssue>();
            CreateMap<CreatePatentVM, Patent>();
            CreateMap<CreatePersonVM, Person>();
        }
    }
}