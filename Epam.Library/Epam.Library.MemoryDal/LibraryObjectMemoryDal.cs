using Epam.Library.DalContracts;
using Epam.Library.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.MemoryDal
{
    public class LibraryObjectMemoryDal : ILibraryObjectDal
    {
        public bool CheckObjectLikeDeletedById(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            int count = Memory.LibraryObjects.Count();
            Memory.LibraryObjects.RemoveAll(p => p.Id == id);

            if (Memory.LibraryObjects.Count() != count)
                return true;
            return false;
        }

        public IEnumerable<LibraryObject> GetAll()
        {
            return Memory.LibraryObjects;
        }

        public IEnumerable<LibraryObject> GetAllDeletedObjects()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<LibraryObject> GetBooksPatentsByPerson(int personId)
        {
            IQueryable<LibraryObject> books = Memory.LibraryObjects.AsQueryable()
            .Where(b => b is Book)
            .Select(b => (Book)b)
            .Where(b => b.Authors.Any(a => a.Id == personId));

            IQueryable<LibraryObject> patents = Memory.LibraryObjects.AsQueryable()
            .Where(b => b is Patent)
            .Select(b => (Patent)b)
            .Where(b => b.Inventors.Any(a => a.Id == personId));

            IEnumerable<LibraryObject> resultCollection = books.Union(patents);

            return resultCollection;
        }

        public IEnumerable<LibraryObject> GetByTitle(string title)
        {
            IQueryable<LibraryObject> libraryObjects = Memory.LibraryObjects.AsQueryable()
                .Where(b => b.Title == title);

            List<LibraryObject> resultCollection = libraryObjects.ToList();

            return resultCollection;
        }
        public ILookup<int, LibraryObject> GroupingByPublishingYear()
        {

            ILookup<int, LibraryObject> resultCollection = Memory.LibraryObjects.ToLookup(p => p.PublishingYear, p => p);

            return resultCollection;
        }

        public bool RestoreObject(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<LibraryObject> SelectLibraryObjectsForPage(int pageNumber, int pageCount)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<LibraryObject> SelectLibraryObjectsForPageSortByYearDirect(int pageNumber, int pageCount)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<LibraryObject> SelectLibraryObjectsForPageSortByYearReverse(int pageNumber, int pageCount)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<LibraryObject> SortingByYearDirectOrder()
        {
            IQueryable<LibraryObject> libraryObjects = Memory.LibraryObjects.AsQueryable()
                .OrderBy(p => p.PublishingYear);

            List<LibraryObject> resultCollection = libraryObjects.ToList();

            return resultCollection;
        }

        public IEnumerable<LibraryObject> SortingByYearReverseOrder()
        {
            IQueryable<LibraryObject> libraryObjects = Memory.LibraryObjects.AsQueryable()
               .OrderByDescending(p => p.PublishingYear);

            List<LibraryObject> resultCollection = libraryObjects.ToList();

            return resultCollection;
        }
    }
}
