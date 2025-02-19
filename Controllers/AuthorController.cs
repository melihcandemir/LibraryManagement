using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class AuthorController : Controller
    {
        // Author list
        public static List<Author> Authors = new List<Author>();

        // Author creation page call method
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Method of adding data from Author creation page to the list
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AuthorAddViewModel formData)
        {

            // ID creation (with empty list check)
            int newAuthorId = 1; // Default ID
            if (Authors.Any()) // If the list is not empty
            {
                newAuthorId = Authors.Max(x => x.Id) + 1;
            }

            var newAuthor = new Author
            {
                Id = newAuthorId,
                FirstName = formData.FirstName,
                LastName = formData.LastName,
                DateOfBirth = formData.BirthDate
            };

            // Checking whether the author you want to add to the list is on the list
            if (Authors.Any(x => x.FirstName.Equals(formData.FirstName)) && Authors.Any(x => x.LastName.Equals(formData.LastName)) && Authors.Any(x => x.DateOfBirth.Equals(formData.BirthDate)))
            {
                ModelState.AddModelError("", "Bu yazar listede mevcut.");
            }

            // If the data sent from the form is valid, it is added to the list.
            if (ModelState.IsValid)
            {
                Authors.Add(newAuthor);
                return View("Feedback", newAuthor);
            }

            return View();
        }

        // method of displaying the authors in the list on the screen
        [HttpGet]
        public IActionResult List()
        {
            var authorViewModel = Authors.Select(x => new AuthorListViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                BirthDate = x.DateOfBirth,
                BookCount = BookController.Books.Count(b => b.AuthorId == x.Id)

            }).ToList();

            return View(authorViewModel);
        }


        // method to update author information page
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var author = Authors.FirstOrDefault(x => x.Id == id);

            var authorEditViewModel = new AuthorEditViewModel
            {
                Id = author?.Id ?? 0,
                FirstName = author?.FirstName,
                LastName = author?.LastName,
                BirthDate = author?.DateOfBirth ?? DateTime.MinValue
            };
            return View(authorEditViewModel);
        }

        // method that updates the data from the author update page in the list
        [HttpPost]
        public IActionResult Edit([FromForm] AuthorEditViewModel formData)
        {
            var author = Authors.FirstOrDefault(x => x.Id == formData.Id) ?? new Author();

            author.FirstName = formData.FirstName;
            author.LastName = formData.LastName;
            author.DateOfBirth = formData.BirthDate;

            return RedirectToAction("List");


        }

        // Page method called to notify the author of the deletion process in the list
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var author = Authors.FirstOrDefault(x => x.Id == id);
            return View("Delete", author);
        }

        // method that deletes the author from the list using the id number of the author to be deleted
        [HttpPost]
        public IActionResult DeleteVote(int id)
        {
            var author = Authors.FirstOrDefault(x => x.Id == id);

            if (author != null)
            {
                // yazar kitaplarını kontrol et ve yazar bilgisini sıfırla
                foreach (var book in BookController.Books.Where(b => b.AuthorId == id))
                {
                    book.AuthorId = -1;
                }

                Authors.Remove(author);
            }

            return RedirectToAction("List");
        }

        // method that calls the page showing author details
        public IActionResult Details(int id)
        {
            var author = Authors.FirstOrDefault(x => x.Id == id);
            var authorBooks = BookController.Books.Where(b => b.AuthorId == id).Select(b => b.Title).ToList();

            var viewModel = new AuthorDetailViewModel
            {
                AuthorName = author?.FullName,
                AuthorBirthDate = author?.DateOfBirth ?? DateTime.MinValue,
                Books = authorBooks
            };

            return View(viewModel);
        }
    }
}