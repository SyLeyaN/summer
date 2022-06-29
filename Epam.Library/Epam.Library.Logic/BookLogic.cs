using Epam.Library.DalContracts;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using Epam.Library.LogicContracts;
using Epam.Library.ValidatorContracts;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.Logic
{
    public class BookLogic : IBookLogic
    {
        private IBookDal _bookDal;
        private IValidator<Book> _validator;

        public BookLogic(IBookDal bookDal, IValidator<Book> validator)
        {
            _bookDal = bookDal;
            _validator = validator;
        }

        public int Add(Book book)
        {
            if (_validator.IsValid(book, out IList<string> validationErrorMessages))
            {
                if (string.IsNullOrEmpty(book.ISBN))
                {
                    book.ISBN = null;
                }
                return _bookDal.Add(book);
            }
            else
            {
                throw new ObjectNotValidateException(validationErrorMessages);
            }
        }

        public ILookup<string, Book> GetAndGroupByPublishingHouse(string publishingHouseFilter)
        {
            return _bookDal.GetAndGroupByPublishingHouse(publishingHouseFilter);
        }

        public IEnumerable<Book> GetByAuthor(int authorId)
        {
            return _bookDal.GetByAuthor(authorId);
        }

        public IEnumerable<Book> GetAll()
        {
            return _bookDal.GetAll();
        }
        public Book GetById(int id)
        {
            return _bookDal.GetById(id);
        }
    }
}
