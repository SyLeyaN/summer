using Epam.Library.Dependencies;
using Epam.Library.Entities;
using Epam.Library.LogicContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTests
{
    [TestClass]
    public class PersonIntegrationTests
    {
        private IPersonLogic _personLogic = DependencyResolver.PersonLogic;
        private Person _correctPerson;

        [TestInitialize]
        public void CorrectPerson()
        {
            _correctPerson = new Person
            {
                Name = "Name",
                Surname = "Surname"
            };
        }

        #region add
        [TestMethod]
        public void AddPersonCorrectValue()
        {
            int? newId = null;
            try
            {
                newId = _personLogic.Add(_correctPerson);
            }
            finally
            {
                _personLogic.Delete((int)newId);
            }

            Assert.IsTrue(newId >= 1);

        }
        #endregion

        #region GetAll

        [TestMethod]
        public void GetAllInNotEmptyCollection()
        {
            int? firstId = null;
            int? secondId = null;
            List<Person> persons = new List<Person>();
            try
            {
                firstId = _personLogic.Add(_correctPerson);
                secondId = _personLogic.Add(new Person { Name = "Namee", Surname = "Ssurname" });
                persons = _personLogic.GetAll().ToList();
            }
            finally
            {
                _personLogic.Delete((int)firstId);
                _personLogic.Delete((int)secondId);
            }

            Assert.IsTrue(persons.Any(p => p.Id == firstId));
            Assert.IsTrue(persons.Any(p => p.Id == firstId));

        }
        #endregion

        #region GetById
        [TestMethod]
        public void GetByCorrectId()
        {
            int? firstId = null;

            Person newPerson = new Person();
            try
            {
                firstId = _personLogic.Add(_correctPerson);
                newPerson = _personLogic.GetById((int)firstId);
            }
            finally
            {
                _personLogic.Delete((int)firstId);
            }
            Assert.IsTrue(newPerson.Id == (int)firstId);


        }

        [TestMethod]
        public void GetByIncorrectId()
        {

            int? firstId = null;

            Person newPerson = new Person();
            try
            {
                firstId = _personLogic.Add(_correctPerson);
                newPerson = _personLogic.GetById((int)firstId + 1);
            }
            finally
            {
                _personLogic.Delete((int)firstId);
            }
            Assert.IsTrue(newPerson.Id == 0);

        }

        #endregion
    }
}
