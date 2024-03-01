using Bookish.Models.Data;
using Bookish.Models.View;
using Microsoft.AspNetCore.Mvc;

namespace Bookish.Controllers;

public class BooksController : Controller
{
    private readonly ILogger<BooksController> _logger;
    private readonly Library _library;

    public BooksController(ILogger<BooksController> logger, Library library)
    {
        _logger = logger;
        _library = library;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ViewAll()
    {
        var books = _library.Books.ToList();
        var viewModel = new BooksViewModel
        {
            Books = books,
        };
        return View(viewModel);
    }

    public IActionResult ViewIndividual([FromRoute] int id)
    {
        var matchingBook = _library.Books.SingleOrDefault(book => book.Id == id);
        if (matchingBook == null)
        {
            return NotFound();
        }
        return View(matchingBook);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register([FromForm] Book book)
    {
        _library.Books.Add(book);
        _library.SaveChanges();
        return RedirectToAction(nameof(ViewAll));
    }
}
