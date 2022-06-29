using Epam.Library.Entities;
using System.Collections.Generic;

namespace Epam.Library.LogicContracts
{
    public interface IPersonLogic
    {
        int Add(Person person);
        Person GetById(int id);
        IEnumerable<Person> GetAll();
        bool Delete(int id);
    }
}
