using AutoMapper;
using Epam.Library.Entities;
using Epam.Library.ViewModels.Display;
using System.Collections.Generic;

namespace Epam.Library.Core.Services
{
    public class DisplayPersonService
    {
        private IMapper _mapper;
        public DisplayPersonService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<DisplayPersonVM> GetPersonVMList(IEnumerable<Person> person)
        {
            return _mapper.Map<IEnumerable<Person>, IEnumerable<DisplayPersonVM>>(person);
        }

    }
}
