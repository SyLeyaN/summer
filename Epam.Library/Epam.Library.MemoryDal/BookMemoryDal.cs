using Epam.Library.DalContracts;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.MemoryDal
{
    public class BookMemoryDal : IBookDal
    {
        public int Add(Book book)
        {
            Book saveBook = new Book
            {
                Title = book.Title,
                NumberOfPages = book.NumberOfPages,
                PublishingCity = book.PublishingCity,
                PublishingHouse = book.PublishingHouse,
                PublishingYear = book.PublishingYear,
                Note = book.Note,
                ISBN = book.ISBN,
                Authors = book.Authors
            };
            saveBook.Id = Memory.NextId;

            if (Uniqueness(saveBook))
            {
                Memory.LibraryObjects.Add(saveBook);
                return saveBook.Id;
            }
            else
                throw new ObjectNotUniqueException();
        }

        private bool Uniqueness(Book book)
        {
            IQueryable<Book> books = Memory.LibraryObjects.AsQueryable()
                    .Where(b => b is Book)
                    .Select(b => (Book)b);

            return !books.Any(b => Equals(book, b));

        }

        public bool Equals(Book book, Book bookInCollection)
        {
            if (!string.IsNullOrEmpty(book.ISBN))
            {
                if (book.ISBN == bookInCollection.ISBN)
                    return true;
            }

            if (book.Title.Equals(bookInCollection.Title) && book.PublishingYear == bookInCollection.PublishingYear)
            {
                if (book.Authors.Count == bookInCollection.Authors.Count)
                {
                    return book.Authors.All(a => bookInCollection.Authors.Any(aInCol => aInCol.Id == a.Id));
                }
            }
            return false;
        }

        public ILookup<string, Book> GetAndGroupByPublishingHouse(string publishingHouseFilter)
        {
            IQueryable<Book> books = Memory.LibraryObjects.AsQueryable()
                .Where(b => b is Book)
                .Select(b => (Book)b)
                .Where(b => b.PublishingHouse.ToLowerInvariant().StartsWith(publishingHouseFilter.ToLowerInvariant()));

            ILookup<string, Book> resultCollection = books.ToLookup(b => b.PublishingHouse, b => b);

            return resultCollection;
        }

        public IEnumerable<Book> GetByAuthor(int authorId)
        {
            return Memory.LibraryObjects.AsQueryable()
                .Where(b => b is Book)
                .Select(b => (Book)b)
                .Where(b => b.Authors.Any(a => a.Id == authorId));
        }

        public IEnumerable<Book> GetAll()
        {
            IEnumerable<LibraryObject> libraryObjects = Memory.LibraryObjects;

            List<Book> books = new List<Book>();
            foreach (LibraryObject libraryObject in libraryObjects)
            {
                if (libraryObject is Book book)
                {
                    books.Add(book);
                }
            }
            return books;
        }

        public Book GetById(int id)
        {
            return (Book)Memory.LibraryObjects
                .FirstOrDefault(b => b.Id == id);
        }
    }
}
