using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repo.App.Entities;

namespace Repo.App.Contracts
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<Genre> GetGenreByIdAsync(Guid genreId);
        Task<Genre> GetGenreWithDetailsAsync(Guid genreId);
        Task CreateGenre(Genre genre);
        void UpdateGenre(Genre genre);
        void DeleteGenre(Genre genre);
    }
}