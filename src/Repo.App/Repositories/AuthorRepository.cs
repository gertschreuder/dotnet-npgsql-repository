using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repo.App.Contracts;
using Repo.App.Entities;

namespace Repo.App.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(SampleContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await FindAll()
                .OrderBy(ow => ow.Name)
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task<Author> GetAuthorByIdAsync(Guid authorId)
        {
            return await FindByCondition(book => book.Id.Equals(authorId))
                .FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<Author> GetAuthorWithDetailsAsync(Guid authorId)
        {
            return await FindByCondition(book => book.Id.Equals(authorId))
                .Include(ac => ac.Book)
                .FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task CreateAuthor(Author author)
        {
            await Create(author).ConfigureAwait(false);
        }

        public void UpdateAuthor(Author author)
        {
            Update(author);
        }

        public void DeleteAuthor(Author author)
        {
            Delete(author);
        }
    }
}