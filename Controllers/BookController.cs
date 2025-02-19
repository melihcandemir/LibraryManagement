using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;


namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        // Book list
        public static List<Book> Books = new List<Book>();

        // method that calls the page to add a book to the list
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // method that adds data from the book adding page to the list
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookAddViewModel formData)
        {
            // ID creation (with empty list check)
            int newBookId = 1; // Default ID
            if (Books.Any()) // If the list is not
            {
                newBookId = Books.Max(x => x.Id) + 1;
            }

            var newBook = new Book
            {
                Id = newBookId,
                AuthorId = formData.AuthorId,
                CopiesAvailable = formData.CopiesAvailable,
                Genre = formData.Genre,
                ISBN = formData.ISBN,
                PublishDate = formData.PublishDate,
                Title = formData.Title
            };

            // Checking whether the added book is the same as the one in the list
            if (Books.Any(x => x.ISBN.Equals(formData.ISBN)))
            {
                ModelState.AddModelError("", "Bu kitap listede mevcut");
            }

            // If the data sent from the form is valid, it is added to the list.
            if (ModelState.IsValid)
            {
                Books.Add(newBook);
                return View("Feedback", newBook);
            }

            return View();
        }

        //method of displaying the books in the list on the screen
        [HttpGet]
        public IActionResult List()
        {
            var bookViewModel = Books.Select(x => new BookListViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Genre = x.Genre,
                CopiesAvailable = x.CopiesAvailable,
                ISBN = x.ISBN,
                PublishDate = x.PublishDate,
                AuthorId = x.AuthorId,
                AuthorName = GetAuthorName(x.AuthorId)
            }).ToList();


            return View(bookViewModel);
        }

        // Method of specifying the author of the book
        private string GetAuthorName(int? authorId)
        {
            if (!authorId.HasValue || authorId.Value <= 0)
                return "Yazar Seçilmedi";
            //
            var author = AuthorController.Authors.FirstOrDefault(a => a.Id == authorId.Value);
            //
            if (author == null)
                return "Yazar Seçilmedi";
            //
            return $"{author.FirstName} {author.LastName}";
        }


        // method to update book information page
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = Books.FirstOrDefault(x => x.Id == id);

            var bookEditViewModel = new BookEditViewModel
            {
                Id = book.Id,
                AuthorId = book.AuthorId,
                Title = book.Title,
                Genre = book.Genre,
                CopiesAvailable = book.CopiesAvailable,
                ISBN = book.ISBN,
                PublishDate = book.PublishDate
            };

            return View(bookEditViewModel);
        }


        // method that updates the data from the book update page in the list
        [HttpPost]
        public IActionResult Edit(BookEditViewModel formData)
        {
            var book = Books.FirstOrDefault(x => x.Id == formData.Id);

            book.Title = formData.Title;
            book.AuthorId = formData.AuthorId;
            book.Genre = formData.Genre;
            book.ISBN = formData.ISBN;
            book.PublishDate = formData.PublishDate;
            book.CopiesAvailable = formData.CopiesAvailable;

            //

            return RedirectToAction("List");
        }


        // method that calls the page showing book details
        public IActionResult Details(int id)
        {
            var book = Books.FirstOrDefault(x => x.Id == id);

            var bookAuthor = AuthorController.Authors.Where(x => x.Id == id);

            var viewBookModel = new BookDetailViewModel
            {
                AuthorName = GetAuthorName(bookAuthor.Select(x => x.Id).FirstOrDefault()),
                CopiesAvailable = book.CopiesAvailable,
                Genre = book.Genre,
                ISBN = book.ISBN,
                PublishDate = book.PublishDate,
                Title = book.Title
            };

            return View(viewBookModel);
        }

        // Page method called to notify the book of the deletion process in the list
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var book = Books.FirstOrDefault(x => x.Id == id);
            return View("Delete", book);
        }

        // method that deletes the book from the list using the id number of the book to be deleted
        [HttpPost]
        public IActionResult DeleteVote(int id)
        {
            var book = Books.FirstOrDefault(x => x.Id == id);
            Books.Remove(book);
            return RedirectToAction("List");
        }
    }
}