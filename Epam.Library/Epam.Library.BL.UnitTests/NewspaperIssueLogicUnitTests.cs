using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BL.UnitTests
{
    [TestClass]
    class NewspaperIssueLogicUnitTests
    {
        #region number
        [TestMethod]
        public void AddWithCorrectNumber() { }

        [TestMethod]
        public void AddWithNumberLess0() { }

        #endregion

        #region publishing date
        [TestMethod]
        public void AddWithCorrectPublishingDate() { }

        [TestMethod]
        public void AddWithIncorrectPublishingDateYear() { }
        #endregion
    }
}
