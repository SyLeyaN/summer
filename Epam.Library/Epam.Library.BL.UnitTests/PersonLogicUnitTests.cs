using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BL.UnitTests
{
    [TestClass]
    class PersonLogicUnitTests
    {
        #region name
        [TestMethod]
        public void AddPersonCorrectNameWithoutHyphenSpace() { }

        [TestMethod]
        public void AddPersonCorrectNameWithHyphen() { }

        [TestMethod]
        public void AddPersonCorrectNameWithHyphenAndBigFirstLetter() { }

        [TestMethod]
        public void AddPersonNoCorrectNameWithHyphenAndSmallLetterAfter() { }

        [TestMethod]
        public void AddPersonNoCorrectNameWithDoubleHyphen() { }

        [TestMethod]
        public void AddPersonNoCorrectNameSmallFirstLetterWithoutHyphen() { }

        #endregion

        #region surname

        [TestMethod]
        public void AddPersonCorrectSurnameWithoutHyphenSpaceApostrofe() { }

        [TestMethod]
        public void AddPersonCorrectSurnameWithSpaceAndSmallFirstLetter() { }

        [TestMethod]
        public void AddPersonNoCorrectSurnameWithSpaceAndBigFirstLetter() { }

        [TestMethod]
        public void AddPersonNoCorrectSurnameWithSpaceAndSmalltLetterAfter() { }

        [TestMethod]
        public void AddPersonCorrectSurnameWithHyphen() { }

        [TestMethod]
        public void AddPersonCorrectSurnameWithHyphenAndSmallFirstLetter() { }

        [TestMethod]
        public void AddPersonCorrectSurnameWithApostrofe() { }

        [TestMethod]
        public void AddPersonCorrectSurnameWithApostrofeAndSmallFirstLetter() { }

        [TestMethod]
        public void AddPersonNoCorrectSurnameWithDoubleSpace() { }

        [TestMethod]
        public void AddPersonNoCorrectSurnameWithDoubleSpaceInDitterentAreas() { }

        [TestMethod]
        public void AddPersonCorrectSurnameWithDoubleApostrofeInDifferentAreas() { }

        [TestMethod]
        public void AddPersonCorrectSurnameWithDoubleHyphenInDifferentAreas() { }

        [TestMethod]
        public void AddPersonNoCorrectSurnameWithDoubleHyphen() { }

        [TestMethod]
        public void AddPersonNoCorrectSurnameWithDoubleApostrofe() { }

        [TestMethod]
        public void AddPersonNoCorrectSurnameSmallFirstLetter() { }

        [TestMethod]
        public void AddPersonNoCorrectSurnameDoubleBigFirstLetters() { }

        [TestMethod]
        public void AddPersonCorrectSpaceApostrofeHyphen() { }

        [TestMethod]
        public void AddPersonCorrectSpaceHyphenApostrofe() { }

        [TestMethod]
        public void AddPersonCorrectHyphenApostrofe() { }

        [TestMethod]
        public void AddPersonCorrectApostrofeHyphen() { }

        [TestMethod]
        public void AddPersonNoCorrectApostrofeHyphenNear() { }

        [TestMethod]
        public void AddPersonNoCorrectHyphenApostrofeNear() { }

        [TestMethod]
        public void AddPersonNoCorrectSpaceApostrofeNear() { }

        [TestMethod]
        public void AddPersonNoCorrectSpaceHyphenNear() { }

        [TestMethod]
        public void AddPersonNoCorrectSpaceFirst() { }

        [TestMethod]
        public void AddPersonNoCorrectSpaceLast() { }

        [TestMethod]
        public void AddPersonNoCorrectHyphenFirst() { }

        [TestMethod]
        public void AddPersonNoCorrectHyphenLast() { }

        [TestMethod]
        public void AddPersonNoCorrectApostrofeFirst() { }

        [TestMethod]
        public void AddPersonNoCorrectApostrofeLast() { }
        #endregion

        #region getAll

        [TestMethod]
        public void CorrectGetAll() { }

        [TestMethod]
        public void GetAllEmpty() { }
        #endregion

        #region getById

        [TestMethod]
        public void CorrectId() { }

        [TestMethod]
        public void NoCorrectId() { }
        #endregion
    }
}
