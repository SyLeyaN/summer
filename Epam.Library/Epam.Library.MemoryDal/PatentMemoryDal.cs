using Epam.Library.DalContracts;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.MemoryDal
{
    public class PatentMemoryDal : IPatentDal
    {
        public int Add(Patent patent)
        {
            Patent savePatent = new Patent
            {
                Title = patent.Title,
                ApplicationDate = patent.ApplicationDate,
                PublishingDate = patent.PublishingDate,
                Country = patent.Country,
                NumberOfPages = patent.NumberOfPages,
                PublishingYear = patent.PublishingYear,
                RegistrationNumber = patent.RegistrationNumber,
                Inventors = patent.Inventors,
                Note = patent.Note
            };

            savePatent.Id = Memory.NextId;

            if (Uniqueness(savePatent))
            {
                Memory.LibraryObjects.Add(savePatent);
                return savePatent.Id;
            }
            else
                throw new ObjectNotUniqueException();
        }

        private bool Uniqueness(Patent patent)
        {
            IQueryable<Patent> patents = Memory.LibraryObjects.AsQueryable()
                    .Where(b => b is Patent)
                    .Select(b => (Patent)b);


            return !patents.Any(p => Equals(patent, p));
        }

        public bool Equals(Patent patent, Patent patentInCollection)
        {
            if (patentInCollection.RegistrationNumber.Equals(patent.RegistrationNumber)
                && patentInCollection.Country.Equals(patent.Country))
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Patent> GetByInventor(int inventorId)
        {
            return Memory.LibraryObjects.AsQueryable()
                    .Where(p => p is Patent)
                    .Select(p => (Patent)p)
                    .Where(p => p.Inventors.Any(a => a.Id == inventorId));
        }

        public IEnumerable<Patent> GetAll()
        {
            IEnumerable<LibraryObject> libraryObjects = Memory.LibraryObjects;

            List<Patent> patents = new List<Patent>();
            foreach (LibraryObject libraryObject in libraryObjects)
            {
                if (libraryObject is Patent patent)
                {
                    patents.Add(patent);
                }
            }
            return patents;
        }

        public Patent GetById(int id)
        {
            return (Patent)Memory.LibraryObjects
                    .Where(b => b.Id == id).FirstOrDefault();
        }
    }
}
