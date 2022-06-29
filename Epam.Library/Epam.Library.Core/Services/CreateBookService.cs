using AutoMapper;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using Epam.Library.LogicContracts;
using Epam.Library.ViewModels.Create;

namespace Epam.Library.Core.Services
{
    public class CreateBookService
    {
        private IMapper _mapper;
        private IBookLogic _bookLogic = Dependencies.DependencyResolver.BookLogic;
        public CreateBookService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public string ResultOfCreateBook(CreateBookVM createBook)
        {
            int? newId = null;
            try
            {
                newId = _bookLogic.Add(_mapper.Map<Book>(createBook));
            }
            catch (ObjectNotValidateException exp)
            {
                var expErrors = exp.BackMessageValidate;
                //errors
                return "Not valid object!";
            }
            catch (ObjectNotUniqueException)
            {
                return "Not unique object!";
            }
            catch
            {
                return "Object has not been added yet! Try again!";
            }

            return null;
        }

    }
}
