using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BL.UnitTests
{
    [TestClass]
    class PatentLogicUnitTests
    {
        #region country
        [TestMethod]
        public void AddWithCorrectCountryWithoutAllBigLetters() { }

        [TestMethod]
        public void AddWithCorrectCountryWithAllBigLetters() { }

        [TestMethod]
        public void AddWithIncorrectCountryWitMore200() { }

        [TestMethod]
        public void AddWithIncorrectCountryWithBigAndSmallLetters() { }

        [TestMethod]
        public void AddWithIncorrectCountryWithSpaceAtTheStart() { }

        [TestMethod]
        public void AddWithIncorrectCountryyWithSpaceAtTheEnd() { }

        [TestMethod]
        public void AddWithIncorrectCountryWithHyphen() { }

        #endregion

        #region title

        [TestMethod]
        public void AddWithCorrectTitle() { }

        [TestMethod]
        public void AddWithMore300Title() { }
        #endregion

        #region application date

        [TestMethod]
        public void AddWithCorrectApplicationDate() { }

        [TestMethod]
        public void AddWithInorrectApplicationDateMoreNow() { }

        [TestMethod]
        public void AddWithInorrectApplicationDateLess1474Year() { }
        #endregion

        #region application and publishing date

        [TestMethod]
        public void AddWithCorrectDates() { }

        [TestMethod]
        public void AddWithInorrectDateMoreNow() { }

        [TestMethod]
        public void AddWithInorrectPublishingDateLessApplication() { }
        #endregion

        #region publishing date

        [TestMethod]
        public void AddWithCorrectPublishingDate() { }

        [TestMethod]
        public void AddWithInorrectPublishingDateMoreNow() { }

        [TestMethod]
        public void AddWithInorrectPublishingDateLess1474Year() { }
        #endregion

        #region publishing year

        [TestMethod]
        public void AddWithCorrectYear() { }

        [TestMethod]
        public void AddWithLess1474Year() { }

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

        #region getByInventor
        [TestMethod]
        public void GetAndGroupByCorrectInventorId() { }

        [TestMethod]
        public void GetAndGroupByInorrectInventorId() { }
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
