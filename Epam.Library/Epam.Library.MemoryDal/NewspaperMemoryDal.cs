using Epam.Library.DalContracts;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.MemoryDal
{
    public class NewspaperMemoryDal : INewspaperDal
    {
        public int Add(Newspaper newspaper)
        {
            Newspaper saveNewspaper = new Newspaper
            {
                Title = newspaper.Title,
                NumberOfPages = newspaper.NumberOfPages,
                PublishingCity = newspaper.PublishingCity,
                PublishingHouse = newspaper.PublishingHouse,
                PublishingYear = newspaper.PublishingYear,
                ISSN = newspaper.ISSN,
                NewspaperIssues = newspaper.NewspaperIssues,
                Note = newspaper.Note
            };

            saveNewspaper.Id = Memory.NextId;

            if (Uniqueness(saveNewspaper))
            {
                Memory.LibraryObjects.Add(saveNewspaper);
                return saveNewspaper.Id;
            }
            else
                throw new ObjectNotUniqueException();
        }

        private bool Uniqueness(Newspaper newspaper)
        {
            IQueryable<Newspaper> newspapers = Memory.LibraryObjects.AsQueryable()
                    .Where(b => b is Newspaper)
                    .Select(b => (Newspaper)b);

            return !newspapers.Any(b => IsDuplicate(newspaper, b));
        }

        public bool IsDuplicate(Newspaper newspaper, Newspaper newspaperInCollection)
        {
            if (!string.IsNullOrEmpty(newspaper.ISSN))
            {
                if (newspaper.ISSN == newspaperInCollection.ISSN)
                {
                    if (newspaper.Title.Equals(newspaperInCollection.Title))
                    {
                        if (newspaper.PublishingHouse.Equals(newspaperInCollection.PublishingHouse))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else if (newspaper.Title.Equals(newspaperInCollection.Title)
                && newspaper.PublishingHouse.Equals(newspaperInCollection.PublishingHouse))
            {
                return true;
            }

            return false;
        }
        public IEnumerable<Newspaper> GetAll()
        {
            IEnumerable<LibraryObject> libraryObjects = Memory.LibraryObjects;

            List<Newspaper> newspapers = new List<Newspaper>();
            foreach (LibraryObject libraryObject in libraryObjects)
            {
                if (libraryObject is Newspaper newspaper)
                {
                    newspapers.Add(newspaper);
                }
            }
            return newspapers;
        }

        public Newspaper GetById(int id)
        {
            return (Newspaper)Memory.LibraryObjects
                     .Where(b => b.Id == id).FirstOrDefault();
        }
    }
}
