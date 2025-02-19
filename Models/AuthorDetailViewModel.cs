namespace LibraryManagement.Models
{
    public class AuthorDetailViewModel
    {
        //
        public string? AuthorName { get; set; }
        //
        public DateTime AuthorBirthDate { get; set; }
        // Author's books
        public List<string>? Books { get; set; }
    }
}