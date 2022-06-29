using Epam.Library.Entities;
using Epam.Library.ValidatorContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Epam.Library.Validator
{
    public class BookValidator : IValidator<Book>
    {
        private static string _cityRegexEng = @"(^[A-Z][a-z]*( ([A-Z]*[a-z]*))*((-[A-Z]+[a-z]+)?|(-[a-z]+-[A-Z]+)?){1}( ([A-Z]*[a-z]*))*[a-z]*$)";
        private static string _cityRegexRus = @"(^[А-ЯЁ][а-яё]*( ([А-ЯЁ]*[а-яё]*))*((-[А-ЯЁ]+[а-яё]+)?|(-[а-яё]+-[А-ЯЁ]+)?){1}( ([А-ЯЁ]*[а-яё]*))*[а-яё]*$)";
        private static string _cityRegex;
        private static string _isbnRegex = @"(^ISBN (([0-7])|(8[0-9]|9[0-4])|(9[5-8][0-9]|99[0-3])|(99[4-8][0-9])|(999[0-9][0-9]))-[0-9]{1,7}-[0-9]{1,7}-([0-9]|X)$)";

        static BookValidator()
        {
            _cityRegex = $"{_cityRegexEng}|{_cityRegexRus}";
        }
        public bool IsValid(Book validationObject, out IList<string> validationErrorMessages)
        {
            validationErrorMessages = new List<string>();

            if (validationObject.Title.Length > 300 || validationObject.Title.Length == 0)
            {
                validationErrorMessages.Add(nameof(validationObject.Title));
            }
            if (!Regex.IsMatch(validationObject.PublishingCity, _cityRegex) || validationObject.PublishingCity.Length > 200)
            {
                validationErrorMessages.Add(nameof(validationObject.PublishingCity));
            }
            if (validationObject.PublishingHouse.Length > 300 || validationObject.PublishingHouse.Length < 0)
            {
                validationErrorMessages.Add(nameof(validationObject.PublishingHouse));
            }
            if (validationObject.PublishingYear > DateTime.Now.Year || validationObject.PublishingYear < 1400)
            {
                validationErrorMessages.Add(nameof(validationObject.PublishingYear));
            }
            if (validationObject.NumberOfPages < 0)
            {
                validationErrorMessages.Add(nameof(validationObject.NumberOfPages));
            }
            if (!string.IsNullOrEmpty(validationObject.Note) && validationObject.Note.Length > 2000)
            {
                validationErrorMessages.Add(nameof(validationObject.Note));
            }
            if (!string.IsNullOrEmpty(validationObject.ISBN) && (!Regex.IsMatch(validationObject.ISBN, _isbnRegex) || validationObject.ISBN.Length != 18))
            {
                validationErrorMessages.Add(nameof(validationObject.ISBN));
            }

            return !validationErrorMessages.Any();
        }
    }
}
