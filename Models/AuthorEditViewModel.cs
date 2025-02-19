using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class AuthorEditViewModel
    {
        //
        public int Id { get; set; }
        //
        public string? FirstName { get; set; }

        //
        public string? LastName { get; set; }
        //
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}