namespace LibraryManagement.Models
{
    public class Book
    {
        // unique id
        public int Id { get; set; }

        // book title
        public string? Title { get; set; }

        // Author ID is referenced from Author model
        public int AuthorId { get; set; }

        // book type
        public string? Genre { get; set; }

        // Publication Year
        public DateTime PublishDate { get; set; }

        // ISBN number
        public string? ISBN { get; set; }

        // number of copies available
        public int CopiesAvailable { get; set; }

    }
}