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

    public IActionResult SearchABook()
    {
        return View();
    }
     
    [HttpPost]
    public IActionResult SearchABook([FromForm] int id)
    {
        var matchingBook = _library.Books.SingleOrDefault(book => book.Id == id);
        if (matchingBook == null)
        {
            return NotFound();
        }
        return  RedirectToAction(nameof(ViewIndividual), new { id = id });
        // return View("/Views/Books/ViewIndividual.cshtml",matchingBook);
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

    // enter from ViewIndividual page
    public IActionResult EditBook([FromRoute] int id)
    {
        var matchingBook = _library.Books.SingleOrDefault(book => book.Id == id);
        if (matchingBook == null)
        {
            return NotFound();
        }
    
       return View();
    }

    [HttpPost]
    public IActionResult EditBook([FromRoute] int id, [FromForm] Book book)
    {
        var matchingBook = _library.Books.SingleOrDefault(book => book.Id == id);
        if (matchingBook == null)
        {
            return NotFound();
        }   
        matchingBook.Title= book.Title;       
        _library.SaveChanges();
        return RedirectToAction(nameof(ViewAll));
    }
    


    // enter from ViewIndividual page
    public IActionResult AddCopy([FromRoute] int id)
    {
        var matchingBook = _library.Books.SingleOrDefault(book => book.Id == id);
        if (matchingBook == null)
        {
            return NotFound();
        }
    
       return View();
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
   
   public IActionResult UnRegister()
    {
        return View();
    }

    [HttpPost]
    public IActionResult UnRegister([FromForm] Book book)
    {
        var matchingBook = _library.Books.SingleOrDefault(eachBook => eachBook.Title == book.Title);
        if (matchingBook == null)
        {
            return NotFound();
        }
        _library.Books.Remove(matchingBook);
        _library.SaveChanges();
        return RedirectToAction(nameof(ViewAll));
    }
   }