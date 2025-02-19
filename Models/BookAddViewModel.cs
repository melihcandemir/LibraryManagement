using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class BookAddViewModel
    {
        //
        public string? Title { get; set; }
        //
        public int AuthorId { get; set; }
        //
        public string? Genre { get; set; }
        //
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        //
        public string? ISBN { get; set; }
        //
        public int CopiesAvailable { get; set; }
    }
}