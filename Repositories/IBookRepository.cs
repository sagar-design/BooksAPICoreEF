using VizippAssignmentCode.Models;

namespace VizippAssignmentCode.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int id);
        Book AddBook(Book book);
        Book? UpdateBook(int id, Book book);
        Book? DeleteBook(int id);
        bool IsIsbnExists(string isbn);
        bool BookExists(int id);
    }
}
