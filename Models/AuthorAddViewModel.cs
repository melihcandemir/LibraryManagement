using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class AuthorAddViewModel
    {
        //
        public string? FirstName { get; set; }

        //
        public string? LastName { get; set; }

        //
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}