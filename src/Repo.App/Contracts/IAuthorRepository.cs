using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repo.App.Entities;

namespace Repo.App.Contracts
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(Guid authorId);
        Task<Author> GetAuthorWithDetailsAsync(Guid authorId);
        Task CreateAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
    }
}