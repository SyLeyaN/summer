using Epam.Library.Dependencies;
using Epam.Library.Entities;
using Epam.Library.LogicContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BL.UnitTests
{
    [TestClass]
    public class BookLogicUnitTests
    {
        private IBookLogic _bookLogic = DependencyResolver.BookLogic;

        private Book _correctBook = new Book();

        [TestInitialize]
        public void CorrectBook()
        {
            _correctBook = new Book();
            _correctBook.Title = "Title";
            _correctBook.PublishingYear = 1900;
            _correctBook.PublishingHouse = "PH";
            _correctBook.PublishingCity = "City";
            _correctBook.NumberOfPages = 10;
            _correctBook.Note = "Note";
            _correctBook.ISBN = "ISBN 6-123456-123456-9";
        }

        #region city
        [TestMethod]
        public void AddWithCorrectCity() {
            Assert.AreEqual(1, _bookLogic.Add(_correctBook));
        }
        
        [TestMethod]
        public void AddWithCorrectCityWithHyphen() { }

        [TestMethod]
        public void AddWithCorrectCityWithDoubleHyphen() { }

        [TestMethod]
        public void AddWithCorrectCityWithSpace() { }

        [TestMethod]
        public void AddWithIncorrectCityWithDoubleHyphenAndBigLetterAfterFirst() { }

        [TestMethod]
        public void AddWithIncorrectCityWithDoubleHyphenAndSmallLetterAfterSecond() { }

        [TestMethod]
        public void AddWithIncorrectCityWithHyphenAndSmallLetterAfter() { }

        [TestMethod]
        public void AddWithIncorrectCityWithSpaceAndSmallLetterAfter() { }

        [TestMethod]
        public void AddWithIncorrectCityWithHyphenAtTheStart() { }

        [TestMethod]
        public void AddWithIncorrectCityWithHyphenAtTheEnd() { }

        [TestMethod]
        public void AddWithIncorrectCityWithHyphenAndSpaceNear() { }

        #endregion

        #region title

        [TestMethod]
        public void AddWithCorrectTitle() { }

        [TestMethod]
        public void AddWithMore300Title() { }
        #endregion

        #region publishing house

        [TestMethod]
        public void AddWithCorrectPublishingHouse() { }

        [TestMethod]
        public void AddWithMore300PublishingHouse() { }

        [TestMethod]
        public void AddWithMoreNowPublishingYear() { }
        #endregion

        #region publishing year

        [TestMethod]
        public void AddWithCorrectYear() { }

        [TestMethod]
        public void AddWithLess1400Year() { }

        [TestMethod]
        public void AddWithPublishingYearMoreNow() { }
        #endregion

        #region number of pages

        [TestMethod]
        public void AddWithCorrectNumberOfPages() { }

        [TestMethod]
        public void AddWithInorrectNumberOfPagesLess0() { }
        #endregion

        #region note

        [TestMethod]
        public void AddWithCorrectNote() { }

        [TestMethod]
        public void AddWithMore2000Note() { }
        #endregion

        #region isbn

        [TestMethod]
        public void AddWithoutISBN() { }

        [TestMethod]
        public void AddWithCorrectISBN() { }

        [TestMethod]
        public void AddWithIncorrectISBNFirst(){ }

        [TestMethod]
        public void AddWithIncorrectISBNSecond() { }

        [TestMethod]
        public void AddWithIncorrectISBNThird() { }

        [TestMethod]
        public void AddWithIncorrectISBNFourth() { }

        [TestMethod]
        public void AddWithIncorrectISBNWithoutSpace() { }
        #endregion

        #region getAndGroupByPublishingHouse
        [TestMethod]
        public void GetAndGroupByCorrectPublishingHouse() { }

        [TestMethod]
        public void GetAndGroupByInorrectPublishingHouse() { }
        #endregion

        #region getByAuthor
        [TestMethod]
        public void GetAndGroupByCorrectAuthorId() { }

        [TestMethod]
        public void GetAndGroupByInorrectAuthorId() { }
        #endregion

        #region getAll 
        [TestMethod]
        public void GetCorrect() { }

        [TestMethod]
        public void GetEmptyCollection() { }
        #endregion

        #region getById
        [TestMethod]
        public void GetByCorrectId() { }

        [TestMethod]
        public void GetByInorrectId() { }
        #endregion

    }
}
