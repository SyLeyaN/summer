using Epam.Library.Entities;
using Epam.Library.ValidatorContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.Validator.UnitTest
{
    [TestClass]
    public class NewspaperValidatorUnitTests
    {
        private IValidator<Newspaper> _validator = new NewspaperValidator();
        private Newspaper _newspaper;

        [TestInitialize]
        public void InitializeCorrectPerson()
        {
            _newspaper = new Newspaper
            {
                Title = "Title",
                NumberOfPages = 10,
                Note = "Note",
                ISSN = "ISSN1234-1234",
                PublishingCity = "City",
                PublishingYear = DateTime.Now.Year,
                PublishingHouse = "House"
            };
        }
        #region city
        [TestMethod]
        public void AddWithCorrectCity()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = _newspaper.PublishingCity,
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = _newspaper.ISSN,
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithCorrectCityWithHyphen()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = "Ci-Ty",
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = _newspaper.ISSN,
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithCorrectCityWithSpace()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = "Ci Ty",
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = _newspaper.ISSN,
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithIncorrectCityWithDoubleHyphen()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = "Ci--ty",
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = _newspaper.ISSN,
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newNewspaper.PublishingCity)));
        }

        [TestMethod]
        public void AddWithIncorrectCityWithHyphenAndSmallLetterAfter()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = "Ci-ty",
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = _newspaper.ISSN,
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newNewspaper.PublishingCity)));
        }

        [TestMethod]
        public void AddWithCorrectCityWithSpaceAndSmallLetterAfter()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = "Bi ty",
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = _newspaper.ISSN,
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithIncorrectCityWithHyphenAtTheStart()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = "-City",
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = _newspaper.ISSN,
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newNewspaper.PublishingCity)));
        }

        [TestMethod]
        public void AddWithIncorrectCityWithHyphenAtTheEnd()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = "City-",
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = _newspaper.ISSN,
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newNewspaper.PublishingCity)));
        }

        [TestMethod]
        public void AddWithIncorrectCityWithHyphenAndSpaceNear()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = "Ci -Ty",
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = _newspaper.ISSN,
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newNewspaper.PublishingCity)));
        }

        #endregion

        #region title

        [TestMethod]
        public void AddWithCorrectTitle()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = _newspaper.PublishingCity,
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = _newspaper.ISSN,
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithMore300Title()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = new string ('s', 301),
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = _newspaper.PublishingCity,
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = _newspaper.ISSN,
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newNewspaper.Title)));
        }
        #endregion

        #region publishing house

        [TestMethod]
        public void AddWithCorrectPublishingHouse()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = _newspaper.PublishingCity,
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = _newspaper.ISSN,
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithMore300PublishingHouse()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = new string('c', 301),
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = _newspaper.PublishingCity,
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = _newspaper.ISSN,
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newNewspaper.PublishingHouse)));
        }
        #endregion

        #region publishing year

        [TestMethod]
        public void AddWithCorrectYear()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = _newspaper.PublishingCity,
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = _newspaper.ISSN,
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithLess1400Year()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = 1300,
                PublishingCity = _newspaper.PublishingCity,
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = _newspaper.ISSN,
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newNewspaper.PublishingYear)));
        }

        [TestMethod]
        public void AddWithMoreNowPublishingYear()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear+1,
                PublishingCity = _newspaper.PublishingCity,
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = _newspaper.ISSN,
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newNewspaper.PublishingYear)));
        }
        #endregion

        #region number of pages

        [TestMethod]
        public void AddWithCorrectNumberOfPages()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = _newspaper.PublishingCity,
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = _newspaper.ISSN,
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithInorrectNumberOfPagesLess0()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = _newspaper.PublishingCity,
                NumberOfPages = -1,
                ISSN = _newspaper.ISSN,
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newNewspaper.NumberOfPages)));
        }
        #endregion

        #region note

        [TestMethod]
        public void AddWithCorrectNote()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = _newspaper.PublishingCity,
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = _newspaper.ISSN,
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithMore2000Note()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = _newspaper.PublishingCity,
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = _newspaper.ISSN,
                Note = new string('c', 2001)
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newNewspaper.Note)));
        }
        #endregion

        #region issn

        [TestMethod]
        public void AddWithoutISSN()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = _newspaper.PublishingCity,
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = "",
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithCorrectISSN()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = _newspaper.PublishingCity,
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = _newspaper.ISSN,
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithIncorrectISSNFirst()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = _newspaper.PublishingCity,
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = "ISSN1-1234",
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newNewspaper.ISSN)));
        }

        [TestMethod]
        public void AddWithIncorrectISSNSecond()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = _newspaper.PublishingCity,
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = "ISSN1234-1",
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newNewspaper.ISSN)));
        }

        [TestMethod]
        public void AddWithIncorrectISSNWithSpace()
        {
            Newspaper newNewspaper = new Newspaper
            {
                Title = _newspaper.Title,
                PublishingHouse = _newspaper.PublishingHouse,
                PublishingYear = _newspaper.PublishingYear,
                PublishingCity = _newspaper.PublishingCity,
                NumberOfPages = _newspaper.NumberOfPages,
                ISSN = "ISSN 1234-1234",
                Note = _newspaper.Note
            };

            bool isValid = _validator.IsValid(newNewspaper, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newNewspaper.ISSN)));
        }
        #endregion

    }
}
