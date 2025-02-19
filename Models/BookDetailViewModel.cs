namespace LibraryManagement.Models
{
    public class BookDetailViewModel
    {
        //
        public string? Title { get; set; }
        //
        public string? AuthorName { get; set; }
        //
        public string? Genre { get; set; }
        //
        public DateTime PublishDate { get; set; }
        //
        public string? ISBN { get; set; }
        //
        public int CopiesAvailable { get; set; }
    }
}