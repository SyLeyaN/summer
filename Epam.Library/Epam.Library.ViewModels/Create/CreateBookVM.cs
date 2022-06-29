using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Epam.Library.ViewModels.Create
{
    public class CreateBookVM : CreateLibraryObjectVM, IValidatableObject
    {
        public List<int> Authors { get; set; } = new List<int>();

        [RegularExpression(@"(^ISBN (([0-7])|(8[0-9]|9[0-4])|(9[5-8][0-9]|99[0-3])|(99[4-8][0-9])|(999[0-9][0-9]))-[0-9]{1,7}-[0-9]{1,7}-([0-9]|X)$)")]
        public string ISBN { get; set; }

        [Required]
        [DisplayName("Publishing city")]
        [RegularExpression(@"(^[A-Z][a-z]*( ([A-Z]*[a-z]*))*((-[A-Z]+[a-z]+)?|(-[a-z]+-[A-Z]+)?){1}( ([A-Z]*[a-z]*))*[a-z]*$)|(^[А-ЯЁ][а-яё]*( ([А-ЯЁ]*[а-яё]*))*((-[А-ЯЁ]+[а-яё]+)?|(-[а-яё]+-[А-ЯЁ]+)?){1}( ([А-ЯЁ]*[а-яё]*))*[а-яё]*$)", ErrorMessage ="Does not comply with the regulations")]
        public string PublishingCity { get; set; }

        [Required]
        [DisplayName("Publishing house")]
        [StringLength(300)]
        public string PublishingHouse { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PublishingYear < 1400)
            {
                yield return new ValidationResult("Publishing year should be more than 1400");
            }
            if (PublishingYear > DateTime.Now.Year)
            {
                yield return new ValidationResult($"Publishing year should be less than {DateTime.Now.Year}");
            }
        }
    }
}
