using Microsoft.AspNetCore.Mvc;
using VizippAssignmentCode.Models;
using VizippAssignmentCode.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace VizippAssignmentCode.Controllers
{
    [ApiController]
    [Route("WebApi/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // Custom path for GET: WebApi/Books/GetAllRecordsFromDatabase
        [HttpGet("GetAllRecordsFromDatabase")]
        public ActionResult<IEnumerable<Book>> GetBooks([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var books = _bookRepository.GetAllBooks();
            var pagedBooks = books.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            if (pagedBooks.Count == 0)
            {
                return NotFound("No books found in the database.");
                //only if not found
            }

            return Ok(pagedBooks);
        }

        // Custom path for GET: WebApi/Books/SearchRecordById/{id}
        [HttpGet("SearchRecordById/{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound($"Book with ID {id} not found in the database.");
            }
            return Ok(book);
        }

        // Custom path for POST: WebApi/Books/CreateNewBook
        [HttpPost("CreateNewBook")]
        public ActionResult<Book> PostBook(Book book)
        {
            if (_bookRepository.IsIsbnExists(book.Isbn))
            {
                return BadRequest("A book with this ISBN already exists.");
            }

            var createdBook = _bookRepository.AddBook(book);
            return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, createdBook);
        }

        // Custom path for PUT: WebApi/Books/Put/UpdateBooksRecord/{id}
        [HttpPut("Put/UpdateBooksRecord/{id}")]
        public IActionResult PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest("ID mismatch. The ID in the URL does not match the ID in the request body.");
            }

            var existingBook = _bookRepository.GetBookById(id);
            if (existingBook == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            // Update the properties of the existing book
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Genre = book.Genre;
            existingBook.Isbn = book.Isbn;

            try
            {
                _bookRepository.UpdateBook(id, existingBook);
            }
            catch
            {
                // Handle any exceptions that may occur
                if (!_bookRepository.BookExists(id))
                {
                    return NotFound($"Book with ID {id} not found.");
                }
                throw;
            }

            return Ok(existingBook);
        }

        // Custom path for DELETE: WebApi/Books/Delete/DeleteBookRecord/{id}
        [HttpDelete("Delete/DeleteBookRecord/{id}")]
        public IActionResult DeleteBook(int id)
        {
            var deletedBook = _bookRepository.DeleteBook(id);
            if (deletedBook == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            return Ok($"Book with ID {id} deleted successfully.");
        }

        // Custom path for GET: WebApi/Books/GetBookWithFilter
        [HttpGet("GetBookWithFilter")]
        public ActionResult<IEnumerable<Book>> FilterBooks([FromQuery] string? genre, [FromQuery] string? author)
        {
            var books = _bookRepository.GetAllBooks();

            if (!string.IsNullOrEmpty(genre))
            {
                books = books.Where(b => b.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(author))
            {
                books = books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!books.Any())
            {
                return NotFound("No books found with the specified filters.");
            }

            return Ok(books);
        }
    }
}
