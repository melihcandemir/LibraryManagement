namespace LibraryManagement.Models
{
    public class Author
    {
        // unique id
        public int Id { get; set; }
        //  author's name
        public string? FirstName { get; set; }
        // Author's surname
        public string? LastName { get; set; }
        // Author's date of birth
        public DateTime DateOfBirth { get; set; }
        // Author's full name for author selection when adding a book
        public string? FullName => $"{FirstName} {LastName}";
    }
}