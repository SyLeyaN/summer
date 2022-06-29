using Epam.Library.DalContracts;
using Epam.Library.Entities;
using Epam.Library.LogicContracts;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.Logic
{
    public class LibraryObjectLogic : ILibraryObjectLogic
    {
        private ILibraryObjectDal _libraryObjectDal;

        public LibraryObjectLogic(ILibraryObjectDal libraryObjectDal)
        {
            _libraryObjectDal = libraryObjectDal;
        }
        public IEnumerable<LibraryObject> SelectLibraryObjectsForPageSortByYearDirect
        (int pageNumber, int pageCount)
        {
            return _libraryObjectDal.SelectLibraryObjectsForPageSortByYearDirect(pageNumber, pageCount);

        }
        public IEnumerable<LibraryObject> SelectLibraryObjectsForPageSortByYearReverse
        (int pageNumber, int pageCount)
        {
            return _libraryObjectDal.SelectLibraryObjectsForPageSortByYearReverse(pageNumber, pageCount);
        }

        public bool CheckObjectLikeDeletedById(int id)
        {
            return _libraryObjectDal.CheckObjectLikeDeletedById(id);
        }

        public bool Delete(int id)
        {
            return _libraryObjectDal.Delete(id);
        }

        public IEnumerable<LibraryObject> GetAll()
        {
            return _libraryObjectDal.GetAll();
        }
        public IEnumerable<LibraryObject> SelectLibraryObjectsForPage(int pageNumber, int pageCount)
        {
            return _libraryObjectDal.SelectLibraryObjectsForPage(pageNumber, pageCount);
        }
        public IEnumerable<LibraryObject> GetAllDeletedObjects()
        {
            return _libraryObjectDal.GetAllDeletedObjects();
        }

        public IEnumerable<LibraryObject> GetBooksPatentsByPerson(int personId)
        {
            return _libraryObjectDal.GetBooksPatentsByPerson(personId);
        }

        public IEnumerable<LibraryObject> GetByTitle(string title)
        {
            return _libraryObjectDal.GetByTitle(title);
        }

        public ILookup<int, LibraryObject> GroupingByPublishingYear()
        {
            return _libraryObjectDal.GroupingByPublishingYear();
        }

        public bool RestoreObject(int id)
        {
            return _libraryObjectDal.RestoreObject(id);
        }

        public IEnumerable<LibraryObject> SortingByYearDirectOrder()
        {
            return _libraryObjectDal.SortingByYearDirectOrder();
        }

        public IEnumerable<LibraryObject> SortingByYearReverseOrder()
        {
            return _libraryObjectDal.SortingByYearReverseOrder();
        }
    }
}
