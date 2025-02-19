using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class BookEditViewModel
    {
        //
        public int Id { get; set; }
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