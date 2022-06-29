using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BL.UnitTests
{
    [TestClass]
    class LibraryObjectLogicUnitTests
    {
        #region delete

        [TestMethod]
        public void DeleteWithCorrectId() { }

        [TestMethod]
        public void DeleteWithIncorrectId() { }

        [TestMethod]
        public void DeleteInEmptyCollection() { }
        #endregion

        #region getAll

        [TestMethod]
        public void GetCorrectCollection() { }

        [TestMethod]
        public void GetEmptyCollection() { }
        #endregion

        #region getBooksPatentsByPerson(){}
        
        [TestMethod]
        public void GetWithCorrectId() { }

        [TestMethod]
        public void GetWithIncorrectId() { }

        [TestMethod]
        public void GetInEmptyCollection() { }
        #endregion

        #region getByTitle

        [TestMethod]
        public void GetWithCorrectTitle() { }

        [TestMethod]
        public void GetWithIncorrectTitle() { }

        [TestMethod]
        public void GetInEmptyCollectionByTitle() { }

        #endregion

        #region GroupingByPublishingYear

        [TestMethod]
        public void GroupCorrectCollection() { }

        [TestMethod]
        public void GroupEmptyCollection() { }

        #endregion

        #region SortingByYearDirectOrder
        [TestMethod]
        public void SortCorrectCollectionDirect() { }

        [TestMethod]
        public void SortEmptyCollectionDirect() { }

        #endregion

        #region SortingByYearReverseOrder
        [TestMethod]
        public void SortCorrectCollectionReverse() { }

        [TestMethod]
        public void SortEmptyCollectionReverse() { }

        #endregion

    }
}
