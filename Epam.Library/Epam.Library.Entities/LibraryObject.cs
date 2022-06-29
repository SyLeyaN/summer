namespace Epam.Library.Entities
{
    public class LibraryObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int NumberOfPages { get; set; }
        public int PublishingYear { get; set; }
        public string Note { get; set; }
        public byte CheckDelete { get; set; }
    }
}
