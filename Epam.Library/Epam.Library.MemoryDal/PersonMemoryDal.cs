using Epam.Library.DalContracts;
using Epam.Library.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.MemoryDal
{
    public class PersonMemoryDal : IPersonDal
    {
        public int Add(Person person)
        {
            Person savePerson = new Person
            {
                Name = person.Name,
                Surname = person.Surname
            };

            savePerson.Id = Memory.NextId;

            Memory.Persons.Add(savePerson);

            return savePerson.Id;
        }

        public IEnumerable<Person> GetAll()
        {
            return Memory.Persons;
        }

        public Person GetById(int id)
        {
            return Memory.Persons.FirstOrDefault(p => p.Id == id);
        }

        public bool Delete(int id)
        {
            int count = Memory.Persons.Count();
            Memory.Persons.RemoveAll(p => p.Id == id);

            if (Memory.Persons.Count() != count)
                return true;
            return false;
        }
    }
}
