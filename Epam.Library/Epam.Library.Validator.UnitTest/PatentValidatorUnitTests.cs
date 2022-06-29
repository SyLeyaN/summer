using Epam.Library.Entities;
using Epam.Library.ValidatorContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.Validator.UnitTest
{
    [TestClass]
    public class PatentValidatorUnitTests
    {
        private IValidator<Patent> _validator = new PatentValidator();
        private Patent _patent;

        [TestInitialize]
        public void InitializeCorrectPerson()
        {
            _patent = new Patent
            {
                Title = "Title",
                PublishingDate = DateTime.Today,
                ApplicationDate = DateTime.Today,
                RegistrationNumber = "123456",
                NumberOfPages = 20,
                Note = "note",
                Country = "Country",
                PublishingYear = 2021
            };
        }

        #region country

        [TestMethod]
        public void AddWithCorrectCountryWithoutAllBigLetters() 
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);

        }

        [TestMethod]
        public void AddWithCorrectCountryWithAllBigLetters()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = "COUNTRY",
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);

        }

        [TestMethod]
        public void AddWithIncorrectCountryWitMore200()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = new string ('c', 201),
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.Country)));

        }

        [TestMethod]
        public void AddWithIncorrectCountryWithBigAndSmallLetters()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = "COUntry",
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.Country)));

        }

        [TestMethod]
        public void AddWithIncorrectCountryWithSpaceAtTheStart()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = " Country",
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.Country)));
        }

        [TestMethod]
        public void AddWithIncorrectCountryyWithSpaceAtTheEnd()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = "Country ",
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.Country)));
        }

        [TestMethod]
        public void AddWithIncorrectCountryWithHyphen()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = "-Country",
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.Country)));
        }

        #endregion

        #region registration number
        [TestMethod]
        public void AddWithCorrectRegistrationNumber()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithIncorrectRegistrationNumberLess0()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = "",
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.RegistrationNumber)));
        }

        [TestMethod]
        public void AddWithIncorrectRegistrationNumberWithLetter()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber+"d",
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.RegistrationNumber)));
        }

        [TestMethod]
        public void AddWithIncorrectRegistrationNumberMore9()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = new string('1', 10),
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.RegistrationNumber)));
        }
        #endregion

        #region title

        [TestMethod]
        public void AddWithCorrectTitle()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithMore300Title()
        {
            Patent newPatent = new Patent
            {
                Title = new string('t', 301),
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.Title)));
        }
        #endregion

        #region application date

        [TestMethod]
        public void AddWithCorrectApplicationDate()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithInorrectApplicationDateMoreNow()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate.AddDays(5),
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(2, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.ApplicationDate)));
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.PublishingDate)));
        }

        [TestMethod]
        public void AddWithInorrectApplicationDateLess1474Year()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = DateTime.Parse("12/12/1400"),
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(2, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.ApplicationDate)));
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.PublishingDate)));
        }
        #endregion

        #region application and publishing date

        [TestMethod]
        public void AddWithCorrectDates()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithInorrectDateMoreNow()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate.AddDays(5),
                ApplicationDate = DateTime.MinValue,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.PublishingDate)));
        }

        [TestMethod]
        public void AddWithInorrectPublishingDateLessApplication()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = DateTime.Parse("12/12/1300"),
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.PublishingDate)));
        }
        #endregion

        #region publishing date

        [TestMethod]
        public void AddWithCorrectPublishingDate()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = DateTime.MinValue,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithInorrectPublishingDateMoreNow()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate.AddDays(5),
                ApplicationDate = DateTime.MinValue,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.PublishingDate)));
        }

        [TestMethod]
        public void AddWithInorrectPublishingDateLess1474Year()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = DateTime.Parse("12/12/1300"),
                ApplicationDate = DateTime.MinValue,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.PublishingDate)));
        }
        #endregion

        #region publishing year

        [TestMethod]
        public void AddWithCorrectYear()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithLess1474Year()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = 1400 
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.PublishingYear)));
        }

        [TestMethod]
        public void AddWithPublishingYearMoreNow()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = DateTime.Now.Year+100
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.PublishingYear)));
        }
        #endregion

        #region number of pages

        [TestMethod]
        public void AddWithCorrectNumberOfPages()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithInorrectNumberOfPagesLess0()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = -100,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.NumberOfPages)));
        }
        #endregion

        #region note

        [TestMethod]
        public void AddWithCorrectNote()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = _patent.Note,
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithMore2000Note()
        {
            Patent newPatent = new Patent
            {
                Title = _patent.Title,
                PublishingDate = _patent.PublishingDate,
                ApplicationDate = _patent.ApplicationDate,
                RegistrationNumber = _patent.RegistrationNumber,
                NumberOfPages = _patent.NumberOfPages,
                Note = new string ('n', 3000),
                Country = _patent.Country,
                PublishingYear = _patent.PublishingYear
            };

            bool isValid = _validator.IsValid(newPatent, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPatent.Note)));
        }
        #endregion

    }
}
