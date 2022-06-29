using Epam.Library.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.DalContracts
{
    public interface ILibraryObjectDal
    {
        IEnumerable<LibraryObject> GetAll();
        bool Delete(int id);
        IEnumerable<LibraryObject> GetByTitle(string title);
        IEnumerable<LibraryObject> SortingByYearDirectOrder();
        IEnumerable<LibraryObject> SortingByYearReverseOrder();
        ILookup<int, LibraryObject> GroupingByPublishingYear(); // key - publishing year
        IEnumerable<LibraryObject> GetBooksPatentsByPerson(int personId);
        bool CheckObjectLikeDeletedById(int id);
        bool RestoreObject(int id);
        IEnumerable<LibraryObject> GetAllDeletedObjects();
        IEnumerable<LibraryObject> SelectLibraryObjectsForPage(int pageNumber, int pageCount);
        IEnumerable<LibraryObject> SelectLibraryObjectsForPageSortByYearDirect
        (int pageNumber, int pageCount);
        IEnumerable<LibraryObject> SelectLibraryObjectsForPageSortByYearReverse
        (int pageNumber, int pageCount);

    }
}
