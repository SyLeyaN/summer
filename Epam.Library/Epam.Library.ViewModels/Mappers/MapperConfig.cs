using AutoMapper;
using Epam.Library.Core.Extensions;
using Epam.Library.Entities;
using Epam.Library.ViewModels.Create;
using Epam.Library.ViewModels.Display;
using System.Linq;

namespace Epam.Library.Mappers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Book, DisplayBookVM>()
                .ForMember(dst => dst.Information, src => src.MapFrom(s => s.DisplayBookInformationInTable()));            
            CreateMap<CreatePersonVM, Person>();
            CreateMap<Person, DisplayPersonVM>()
                .ForMember(dst => dst.NameSurname, src => src.MapFrom(s => $"{s.Name} {s.Surname}"));
            CreateMap<CreateBookVM, Book>()
                .ForMember(dst => dst.Authors, src => src.MapFrom(s => s.Authors.Select(id => new Person { Id = id })));
        }
    }
}