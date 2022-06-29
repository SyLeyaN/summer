using Epam.Library.Entities;
using Epam.Library.ValidatorContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.Validator.UnitTest
{
    [TestClass]
    public class PersonValidatorUnitTests
    {
        private IValidator<Person> _validator = new PersonValidator();
        private Person _person;
        [TestInitialize]
        public void InitializeCorrectPerson()
        {
            _person = new Person
            {
                Name = "Name",
                Surname = "Surname"
            };
        }

        #region name
        [TestMethod]
        public void AddPersonCorrectNameWithoutHyphenSpace()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = _person.Surname
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddPersonCorrectNameWithHyphen() 
        {
            Person newPerson = new Person
            {
                Name = "Na-Me",
                Surname = _person.Surname
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddPersonCorrectNameWithHyphenAndBigFirstLetter() 
        {
            Person newPerson = new Person
            {
                Name = "Na-Me",
                Surname = _person.Surname
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddPersonNoCorrectNameWithHyphenAndSmallLetterAfter()
        {
            Person newPerson = new Person
            {
                Name = "Na-me",
                Surname = _person.Surname
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Name)));
        }

        [TestMethod]
        public void AddPersonNoCorrectNameWithDoubleHyphen()
        {
            Person newPerson = new Person
            {
                Name = "Na--me",
                Surname = _person.Surname
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Name)));
        }

        [TestMethod]
        public void AddPersonNoCorrectNameSmallFirstLetterWithoutHyphen()
        {
            Person newPerson = new Person
            {
                Name = "name",
                Surname = _person.Surname
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Name)));
        }

        #endregion

        #region surname

        [TestMethod]
        public void AddPersonCorrectSurnameWithoutHyphenSpaceApostrofe()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = _person.Surname
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }


        [TestMethod]
        public void AddPersonCorrectSurnameWithSpaceAndSmallFirstLetter()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "sur Name"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }
        [TestMethod]
        public void AddPersonNoCorrectSurnameWithSpaceAndBigFirstLetter()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "Sur Name"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonNoCorrectSurnameWithSpaceAndSmalltLetterAfter()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "sur name"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonCorrectSurnameWithHyphen()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "Sur-Name"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddPersonCorrectSurnameWithHyphenAndSmallFirstLetter()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "Sur-name"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonCorrectSurnameWithApostrofe()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "Sur'Name"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddPersonNoCorrectSurnameWithApostrofeAndSmallFirstLetter()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "Su'name"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonNoCorrectSurnameWithDoubleSpace()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "Sur  Name"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonNoCorrectSurnameWithDoubleSpaceInDitterentAreas()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "Sur Na me"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonCorrectSurnameWithDoubleApostrofeInDifferentAreas()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "Sur'Na'me"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonCorrectSurnameWithDoubleHyphenInDifferentAreas()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "Sur-Na-me"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonNoCorrectSurnameWithDoubleHyphen()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "Sur--Name"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonNoCorrectSurnameWithDoubleApostrofe()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "Sur''Name"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonNoCorrectSurnameSmallFirstLetter()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "surname"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonNoCorrectSurnameDoubleBigFirstLetters()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "SUrname"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonCorrectSpaceApostrofeHyphen()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "Sur'-Name"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonCorrectSpaceHyphenApostrofe()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "sur Na-Me'Es"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddPersonCorrectHyphenApostrofe()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "Sur-Na'Me"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddPersonCorrectApostrofeHyphen()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "Sur'Na-Me"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddPersonNoCorrectApostrofeHyphenNear()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "Sur'-Name"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonNoCorrectHyphenApostrofeNear()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "Sur-'Name"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonNoCorrectSpaceApostrofeNear()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "sur 'Name"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonNoCorrectSpaceHyphenNear()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "sur -Name"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonNoCorrectSpaceFirst()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = " Surname"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonNoCorrectSpaceLast()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "Surname "
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonNoCorrectHyphenFirst()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "-Surname"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonNoCorrectHyphenLast()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "Surname-"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonNoCorrectApostrofeFirst()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "'Surname"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }

        [TestMethod]
        public void AddPersonNoCorrectApostrofeLast()
        {
            Person newPerson = new Person
            {
                Name = _person.Name,
                Surname = "Surname'"
            };
            bool isValid = _validator.IsValid(newPerson, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newPerson.Surname)));
        }
        #endregion

    }
}
