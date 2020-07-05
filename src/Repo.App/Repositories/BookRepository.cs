using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repo.App.Contracts;
using Repo.App.Entities;

namespace Repo.App.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(SampleContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await FindAll()
                .OrderBy(ow => ow.Name)
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task<Book> GetBookByIdAsync(Guid bookId)
        {
            return await FindByCondition(book => book.Id.Equals(bookId))
                .FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<Book> GetBookWithDetailsAsync(Guid bookId)
        {
            return await FindByCondition(book => book.Id.Equals(bookId))
                .Include(ac => ac.Author)
                .Include(ac => ac.Genre)
                .FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task CreateBook(Book book)
        {
            await Create(book).ConfigureAwait(false);
        }

        public void UpdateBook(Book book)
        {
            Update(book);
        }

        public void DeleteBook(Book book)
        {
            Delete(book);
        }
    }
}