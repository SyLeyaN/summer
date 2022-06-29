using Epam.Library.Entities;
using Epam.Library.ValidatorContracts;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Epam.Library.Validator
{
    public class PersonValidator : IValidator<Person>
    {
        private static string _nameRegexEng = @"(^[A-Z][a-z]*(-[A-Z])?[a-z]*$)";
        private static string _nameRegexRus = @"(^[А-ЯЁ][а-яё]*(-[А-ЯЁ])?[а-яё]*$)";
        private static string _nameRegex;

        private static string _surnameRegexEng = @"(^([a-z]+ )?[A-Z][a-z]*((-[A-Z][a-z]*)?('[A-Z][a-z]*)?)*[a-z]*$)";
        private static string _surnameRegexRus = @"(^([а-яё]+ )?[А-ЯЁ][а-яё]*((-[А-ЯЁ][а-яё]*)?('[А-ЯЁ][а-яё]*)?)*[а-яё]*$)";
        private static string _surnameRegex;

        static PersonValidator()
        {
            _nameRegex = $"{_nameRegexEng}|{_nameRegexRus}";
            _surnameRegex = $"{_surnameRegexEng}|{_surnameRegexRus}";
        }

        public bool IsValid(Person validationObject, out IList<string> validationErrorMessages)
        {
            validationErrorMessages = new List<string>();

            if (!Regex.IsMatch(validationObject.Name, _nameRegex))
            {
                validationErrorMessages.Add(nameof(validationObject.Name));
            }
            if (!Regex.IsMatch(validationObject.Surname, _surnameRegex))
            {
                validationErrorMessages.Add(nameof(validationObject.Surname));
            }

            return !validationErrorMessages.Any();
        }
    }
}
