using Epam.Library.Dependencies;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using Epam.Library.LogicContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTests
{
    [TestClass]
    public class PatentIntegrationTests
    {
        private IPatentLogic _patentLogic = DependencyResolver.PatentLogic;
        private IPersonLogic _personLogic = DependencyResolver.PersonLogic;
        private Patent _correctPatent;
        private Person _correctPerson;
        private ILibraryObjectLogic libraryObjectLogic = DependencyResolver.LibraryObjectLogic;

        [TestInitialize]
        public void CorrectPatent()
        {
            _correctPatent = new Patent
            {
                Title = "Title",
                NumberOfPages = 1,
                ApplicationDate = DateTime.MinValue,
                PublishingDate = DateTime.Now,
                PublishingYear = DateTime.Now.Year,
                Country = "Country",
                Note = "Note",
                RegistrationNumber = "123456"
            };
            _correctPerson = new Person
            {
                Name = "Name",
                Surname = "Surname"
            };

        }

        #region add
        [TestMethod]
        public void AddPatentCorrectValue()
        {
            int newId = _patentLogic.Add(_correctPatent);

            libraryObjectLogic.Delete(newId);

            Assert.IsTrue(newId >= 1);

        }

        [TestMethod]
        public void AddTwoDifferentPatents()
        {
            int firstId = _patentLogic.Add(_correctPatent);
            int secondId = _patentLogic.Add(new Patent
            {
                Title = "Titleee",
                NumberOfPages = 1,
                ApplicationDate = DateTime.Now,
                PublishingDate = DateTime.Now,
                PublishingYear = DateTime.Now.Year,
                Country = "Countryy",
                Note = "Note",
                RegistrationNumber = "123456"
            });

            libraryObjectLogic.Delete(firstId);
            libraryObjectLogic.Delete(secondId);

            Assert.IsTrue(firstId >= 1);
            Assert.IsTrue(secondId > firstId);

        }

        [DataTestMethod]
        public void AddTwoSamePatentsExpectException()
        {
            Patent seconCorrectPatent = new Patent
            {
                Title = "Title",
                NumberOfPages = 1,
                ApplicationDate = DateTime.MinValue,
                PublishingDate = DateTime.Now,
                PublishingYear = DateTime.Now.Year,
                Country = "Country",
                Note = "Note",
                RegistrationNumber = "123456"
            };

            int firstId = _patentLogic.Add(_correctPatent);
            string error = null;
            int? secondId = 0;
            try
            {
                secondId = _patentLogic.Add(seconCorrectPatent);
            }
            catch (ObjectNotUniqueException e)
            {
                error = e.Message;
            }
            finally
            {
                libraryObjectLogic.Delete(firstId);
                if (secondId != null)
                {
                    libraryObjectLogic.Delete((int)secondId);
                }
            }
            Assert.IsTrue(error != null);
        }

        [TestMethod]
        public void AddTwoPatentsSameRegistrationNumberAndCountryExpectException()
        {
            int firstId = _patentLogic.Add(_correctPatent);
            string error = null;
            int secondId = 0;
            try
            {
                secondId = _patentLogic.Add(new Patent
                {
                    Title = "Titlee",
                    NumberOfPages = 1,
                    ApplicationDate = DateTime.Now,
                    PublishingDate = DateTime.Now,
                    PublishingYear = DateTime.Now.Year,
                    Country = "Country",
                    Note = "Note",
                    RegistrationNumber = "123456"
                });
            }
            catch (ObjectNotUniqueException e)
            {
                error = e.Message;
            }

            libraryObjectLogic.Delete(firstId);
            libraryObjectLogic.Delete(secondId);

            Assert.IsTrue(error != null);
        }
        #endregion

        #region GetByInventor
        [TestMethod]
        public void GetByCorrectInventor()
        {
            int personId = _personLogic.Add(_correctPerson);
            _correctPatent.Inventors.Add(_correctPerson);

            int? firstId = null;

            List<Patent> patents = new List<Patent>();
            try
            {
                firstId = _patentLogic.Add(_correctPatent);
                patents = _patentLogic.GetByInventor(personId).ToList();
            }
            finally
            {
                libraryObjectLogic.Delete((int)firstId);
                _personLogic.Delete(personId);
            }
            Assert.IsTrue(patents.Any(p => p.Id == firstId));

        }

        [TestMethod]
        public void GetByIncorrectInventor()
        {
            int personId = _personLogic.Add(_correctPerson);
            _correctPatent.Inventors.Add(_correctPerson);
            int? firstId = null;

            List<Patent> patents = new List<Patent>();
            try
            {
                firstId = _patentLogic.Add(_correctPatent);
                patents = _patentLogic.GetByInventor(personId + 1).ToList();
            }
            finally
            {
                libraryObjectLogic.Delete((int)firstId);
                _personLogic.Delete(personId);
            }
            Assert.IsTrue(!patents.Any(p => p.Id == firstId));
            Assert.IsTrue(patents.Count() == 0);

        }

        #endregion

        #region GetAll

        [TestMethod]
        public void GetAllInNotEmptyCollection()
        {
            int? firstId = null;
            List<Patent> patents = new List<Patent>();
            try
            {
                firstId = _patentLogic.Add(_correctPatent);
                patents = _patentLogic.GetAll().ToList();
            }
            finally
            {
                libraryObjectLogic.Delete((int)firstId);
            }
            Assert.IsTrue(patents.Any(p => p.Id == firstId));

        }

        #endregion

        #region GetById
        [TestMethod]
        public void GetByCorrectId()
        {
            int? firstId = null;
            Patent patent = new Patent();
            try
            {
                firstId = _patentLogic.Add(_correctPatent);
                patent = _patentLogic.GetById((int)firstId);
            }
            finally
            {
                libraryObjectLogic.Delete((int)firstId);
            }
            Assert.IsTrue(patent.Id == firstId);

        }

        [TestMethod]
        public void GetByIncorrectId()
        {
            int? firstId = null;
            Patent patent = new Patent();
            try
            {
                firstId = _patentLogic.Add(_correctPatent);
                patent = _patentLogic.GetById((int)firstId + 1);
            }
            finally
            {
                libraryObjectLogic.Delete((int)firstId);
            }
            Assert.IsTrue(patent.Id == 0);

        }
        #endregion
    }
}
