using Epam.Library.Entities;
using System.Collections.Generic;

namespace Epam.Library.DalContracts
{
    public interface IPersonDal
    {
        int Add(Person person);
        Person GetById(int id);
        IEnumerable<Person> GetAll();
        bool Delete(int id);
    }
}
