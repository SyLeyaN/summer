using Epam.Library.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.DalContracts
{
    public interface IBookDal
    {
        int Add(Book book);
        IEnumerable<Book> GetByAuthor(int authorId);
        ILookup<string, Book> GetAndGroupByPublishingHouse(string publishingHouseFilter); //key - publishing house
        IEnumerable<Book> GetAll();
        Book GetById(int id);
    }
}
