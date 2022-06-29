using AutoMapper;
using Epam.Library.Entities;
using Epam.Library.ViewModels.Display;
using System.Collections.Generic;

namespace Epam.Library.Core.Services
{
    public class GetAllService
    {
        private IMapper _mapper;
        public GetAllService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public List<DisplayLibraryObjectVM> DisplayList(IEnumerable<LibraryObject> objs)
        {
            List<DisplayLibraryObjectVM> resulListForDisplay = new List<DisplayLibraryObjectVM>();
            foreach (var obj in objs)
            {
                if (obj is Book)
                {
                    resulListForDisplay.Add(_mapper.Map<DisplayBookVM>((Book)obj));
                }
                if (obj is Patent)
                {
                    resulListForDisplay.Add(_mapper.Map<Patent, DisplayPatentVM>((Patent)obj));
                }
                if (obj is Newspaper)
                {
                    resulListForDisplay.Add(_mapper.Map<Newspaper, DisplayNewspaperVM>((Newspaper)obj));
                }
            }
            return resulListForDisplay;
        }
    }
}