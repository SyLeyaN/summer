using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Epam.Library.ViewModels.Create
{
    public abstract class CreateLibraryObjectVM
    {
        [Required]
        [StringLength(300)]
        public string Title { get; set; }

        [Required]
        [DisplayName("Number of pages")]
        public int NumberOfPages { get; set; }

        [StringLength(2000)]
        public string Note { get; set; }

        [Required]
        [DisplayName("Publishing year")]
        public int PublishingYear { get; set; }
    }
}
