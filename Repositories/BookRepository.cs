using Microsoft.EntityFrameworkCore;
using VizippAssignmentCode.Models;

namespace VizippAssignmentCode.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;

        public BookRepository(BookContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book GetBookById(int id)
        {
            return _context.Books.Find(id);
        }

        public Book AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }

        public Book UpdateBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return null; // ID mismatch
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                return book;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency issues
                if (!BookExists(id))
                {
                    return null; // Book not found
                }
                throw;
            }
        }

        public Book DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null) return null; // Book not found

            _context.Books.Remove(book);
            _context.SaveChanges();
            return book;
        }

        public bool IsIsbnExists(string isbn)
        {
            return _context.Books.Any(b => b.Isbn == isbn);
        }

        public bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
