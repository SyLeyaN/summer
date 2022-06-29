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
    public class NewspaperIntegrationTests
    {
        private INewspaperLogic _newspaperLogic = DependencyResolver.NewspaperLogic;
        private Newspaper _correctNewspaper;
        private ILibraryObjectLogic libraryObjectLogic = DependencyResolver.LibraryObjectLogic;

        [TestInitialize]
        public void CorrectNewspaper()
        {
            _correctNewspaper = new Newspaper
            {
                Title = "Title",
                NumberOfPages = 1,
                PublishingYear = DateTime.Now.Year,
                Note = "Note",
                PublishingCity = "City",
                PublishingHouse = "House",
                ISSN = "ISSN1234-1234"
            };
        }

        #region add
        [TestMethod]
        public void AddNewspaperCorrectValue()
        {
            int? newId = null;
            try
            {
                newId = _newspaperLogic.Add(_correctNewspaper);
            }
            finally
            {
                libraryObjectLogic.Delete((int)newId);
            }
            Assert.IsTrue(newId >= 1);
        }

        [TestMethod]
        public void AddTwoDifferentNewspapers()
        {
            int? firstId = null;
            int? secondId = null;
            try
            {
                firstId = _newspaperLogic.Add(_correctNewspaper);
                secondId = _newspaperLogic.Add(new Newspaper
                {
                    Title = "Titlee",
                    NumberOfPages = 1,
                    PublishingYear = DateTime.Now.Year - 1,
                    Note = "Note",
                    PublishingCity = "Cityy",
                    PublishingHouse = "Housee",
                    ISSN = ""
                });
            }
            finally
            {
                libraryObjectLogic.Delete((int)firstId);
                libraryObjectLogic.Delete((int)secondId);
            }
            Assert.IsTrue(firstId >= 1);
            Assert.IsTrue(secondId > firstId);

        }

        [TestMethod]
        public void AddTwoNewspapersSameTitlePublishingHouseExpectException()
        {
            int? firstId = null;
            int? secondId = null;

            string error = null;

            try
            {
                firstId = _newspaperLogic.Add(new Newspaper
                {
                    Title = "Titlee",
                    NumberOfPages = 1,
                    PublishingYear = DateTime.Now.Year,
                    Note = "Note",
                    PublishingCity = "City",
                    PublishingHouse = "House",
                    ISSN = ""
                });

                secondId = _newspaperLogic.Add(new Newspaper
                {
                    Title = "Titlee",
                    NumberOfPages = 2,
                    PublishingYear = DateTime.Now.Year,
                    Note = "Note",
                    PublishingCity = "City",
                    PublishingHouse = "House",
                    ISSN = ""
                });
            }
            catch (ObjectNotUniqueException e)
            {
                error = e.Message;
            }
            finally
            {
                libraryObjectLogic.Delete((int)firstId);
                if (secondId != null)
                {
                    libraryObjectLogic.Delete((int)secondId);
                }
            }

            Assert.IsNotNull(error);
        }
        #endregion

        #region GetAll
        [TestMethod]
        public void GetAllInNotEmptyCollection()
        {
            int? firstId = null;
            List<Newspaper> newspapers = new List<Newspaper>();

            try
            {
                firstId = _newspaperLogic.Add(_correctNewspaper);
                newspapers = _newspaperLogic.GetAll().ToList();
            }
            finally
            {
                libraryObjectLogic.Delete((int)firstId);
            }

            Assert.IsTrue(newspapers.Any(p => p.Id == firstId));

        }

        #endregion

        #region GetById
        [TestMethod]
        public void GetByCorrectId()
        {
            int? firstId = null;
            Newspaper newspaper = new Newspaper();
            try
            {
                firstId = _newspaperLogic.Add(_correctNewspaper);
                newspaper = _newspaperLogic.GetById((int)firstId);
            }
            finally
            {
                libraryObjectLogic.Delete((int)firstId);
            }
            Assert.IsTrue(newspaper.Id == firstId);
            Assert.IsNotNull(newspaper);

        }

        [TestMethod]
        public void GetByIncorrectId()
        {
            int? firstId = null;
            Newspaper newspaper = new Newspaper();
            try
            {
                firstId = _newspaperLogic.Add(_correctNewspaper);
                newspaper = _newspaperLogic.GetById((int)firstId + 1);
            }
            finally
            {
                libraryObjectLogic.Delete((int)firstId);
            }

            Assert.IsTrue(newspaper.Id == 0);
        }
        #endregion

    }
}
