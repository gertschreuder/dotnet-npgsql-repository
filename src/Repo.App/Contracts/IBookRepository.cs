using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repo.App.Entities;

namespace Repo.App.Contracts
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(Guid bookId);
        Task<Book> GetBookWithDetailsAsync(Guid bookId);
        Task CreateBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
    }
}