using Epam.Library.Entities;
using Epam.Library.ValidatorContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.Validator.UnitTest
{
    [TestClass]
    public class BookValidatorUnitTests
    {
        private IValidator<Book> _validator = new BookValidator();

        private Book _correctBook;

        [TestInitialize]
        public void CorrectBook()
        {
            _correctBook = new Book
            {
                Title = "Title",
                PublishingYear = 1900,
                PublishingHouse = "Pubhouse",
                PublishingCity = "City",
                NumberOfPages = 10,
                Note = "Note",
                ISBN = "ISBN 6-1256-1356-9"
            };
        }
        #region city
        [TestMethod]
        public void AddWithCorrectCity()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = _correctBook.PublishingCity,
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithCorrectCityWithHyphen()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = "Ci-Ty",
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }


        [TestMethod]
        public void AddWithCorrectCityWithDoubleHyphen()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = "Ci-tt-Yy",
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }


        [TestMethod]
        public void AddWithCorrectCityWithSpace()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = "Ci ty",
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }


        [TestMethod]
        public void AddWithIncorrectCityWithDoubleHyphenAndBigLetterAfterFirst()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = "Ci-Tt-Yy",
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newBook.PublishingCity)));
        }


        [TestMethod]
        public void AddWithIncorrectCityWithDoubleHyphenAndSmallLetterAfterSecond()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = "Ci-tt-yy",
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newBook.PublishingCity)));
        }


        [TestMethod]
        public void AddWithIncorrectCityWithHyphenAndSmallLetterAfter()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = "Ci-tty",
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newBook.PublishingCity)));
        }

        [TestMethod]
        public void AddWithCorrectorrectCityWithSpaceAndSmallLetterAfter()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = "Ci yy",
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithIncorrectCityWithHyphenAtTheStart()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = "-Ciyy",
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newBook.PublishingCity)));
        }

        [TestMethod]
        public void AddWithIncorrectCityWithHyphenAtTheEnd()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = "Cyy-",
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newBook.PublishingCity)));
        }

        [TestMethod]
        public void AddWithIncorrectCityWithHyphenAndSpaceNear()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = "Ci-tt- yy",
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newBook.PublishingCity)));
        }

        #endregion

        #region title

        [TestMethod]
        public void AddWithCorrectTitle()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = _correctBook.PublishingCity,
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithMore300Title()
        {
            Book newBook = new Book
            {
                Title = new string('d', 301),
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = _correctBook.PublishingCity,
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newBook.Title)));
        }
        #endregion

        #region publishing house

        [TestMethod]
        public void AddWithCorrectPublishingHouse()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = _correctBook.PublishingCity,
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithMore300PublishingHouse()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = new string('j', 301),
                PublishingCity = _correctBook.PublishingCity,
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newBook.PublishingHouse)));
        }


        #endregion

        #region publishing year

        [TestMethod]
        public void AddWithCorrectYear()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = _correctBook.PublishingCity,
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithLess1400Year()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = 1000,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = _correctBook.PublishingCity,
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newBook.PublishingYear)));
        }

        [TestMethod]
        public void AddWithMoreNowPublishingYear()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = DateTime.Now.Year + 1,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = _correctBook.PublishingCity,
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newBook.PublishingYear)));
        }
        #endregion

        #region number of pages

        [TestMethod]
        public void AddWithCorrectNumberOfPages()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = _correctBook.PublishingCity,
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithInorrectNumberOfPagesLess0()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = _correctBook.PublishingCity,
                NumberOfPages = -1,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newBook.NumberOfPages)));
        }
        #endregion

        #region note

        [TestMethod]
        public void AddWithCorrectNote()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = _correctBook.PublishingCity,
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithMore2000Note()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = _correctBook.PublishingCity,
                NumberOfPages = _correctBook.NumberOfPages,
                Note = new string('b', 2001),
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newBook.Note)));
        }
        #endregion

        #region isbn

        [TestMethod]
        public void AddWithoutISBN()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = _correctBook.PublishingCity,
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = ""
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithCorrectISBN()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = _correctBook.PublishingCity,
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = _correctBook.ISBN
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithIncorrectISBNFirst()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = _correctBook.PublishingCity,
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = "ISBN 100-1256-1356-9"
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newBook.ISBN)));
        }

        [TestMethod]
        public void AddWithIncorrectISBNSecond()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = _correctBook.PublishingCity,
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = "ISBN 7-126-1356-9"
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newBook.ISBN)));
        }


        [TestMethod]
        public void AddWithIncorrectISBNThird()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = _correctBook.PublishingCity,
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = "ISBN 7-1256-156-9"
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newBook.ISBN)));
        }


        [TestMethod]
        public void AddWithIncorrectISBNFourth()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = _correctBook.PublishingCity,
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = "ISBN 7-1256-1356-100"
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newBook.ISBN)));
        }


        [TestMethod]
        public void AddWithIncorrectISBNWithoutSpace()
        {
            Book newBook = new Book
            {
                Title = _correctBook.Title,
                PublishingYear = _correctBook.PublishingYear,
                PublishingHouse = _correctBook.PublishingHouse,
                PublishingCity = _correctBook.PublishingCity,
                NumberOfPages = _correctBook.NumberOfPages,
                Note = _correctBook.Note,
                ISBN = "ISBN7-1256-1356-9"
            };

            bool isValid = _validator.IsValid(newBook, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newBook.ISBN)));
        }

        #endregion
    }
}
