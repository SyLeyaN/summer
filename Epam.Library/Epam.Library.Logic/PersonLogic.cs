using Epam.Library.DalContracts;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using Epam.Library.LogicContracts;
using Epam.Library.ValidatorContracts;
using System.Collections.Generic;

namespace Epam.Library.Logic
{
    public class PersonLogic : IPersonLogic
    {
        private IPersonDal _personDal;
        private IValidator<Person> _validator;

        public PersonLogic(IPersonDal personDal, IValidator<Person> validator)
        {
            _personDal = personDal;
            _validator = validator;
        }

        public int Add(Person person)
        {
            if (_validator.IsValid(person, out IList<string> validationErrorMessages))
            {
                return _personDal.Add(person);
            }
            else
            {
                throw new ObjectNotValidateException(validationErrorMessages);
            }
        }

        public IEnumerable<Person> GetAll()
        {
            return _personDal.GetAll();
        }

        public Person GetById(int id)
        {
            return _personDal.GetById(id);
        }

        public bool Delete(int id)
        {
            return _personDal.Delete(id);
        }
    }
}
