using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BL.UnitTests
{
    [TestClass]
    class NewspaperLogicUnitTests
    {
        #region city
        [TestMethod]
        public void AddWithCorrectCity() { }

        [TestMethod]
        public void AddWithCorrectCityWithHyphen() { }

        [TestMethod]
        public void AddWithCorrectCityWithSpace() { }

        [TestMethod]
        public void AddWithIncorrectCityWithDoubleHyphen() { }

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
        #endregion

        #region publishing year

        [TestMethod]
        public void AddWithCorrectYear() { }

        [TestMethod]
        public void AddWithLess1400Year() { }

        [TestMethod]
        public void AddWithMoreNowPublishingYear() { }
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

        #region issn

        [TestMethod]
        public void AddWithoutISSN() { }

        [TestMethod]
        public void AddWithCorrectISSN() { }

        [TestMethod]
        public void AddWithIncorrectISSNFirst() { }

        [TestMethod]
        public void AddWithIncorrectISSNSecond() { }

        [TestMethod]
        public void AddWithIncorrectISSNWithSpace() { }
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
