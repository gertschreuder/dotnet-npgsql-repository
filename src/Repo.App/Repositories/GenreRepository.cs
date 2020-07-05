using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repo.App.Contracts;
using Repo.App.Entities;

namespace Repo.App.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(SampleContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await FindAll()
                .OrderBy(ow => ow.Name)
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task<Genre> GetGenreByIdAsync(Guid genreId)
        {
            return await FindByCondition(book => book.Id.Equals(genreId))
                .FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<Genre> GetGenreWithDetailsAsync(Guid genreId)
        {
            return await FindByCondition(book => book.Id.Equals(genreId))
                .Include(ac => ac.Book)
                .FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task CreateGenre(Genre genre)
        {
            await Create(genre).ConfigureAwait(false);
        }

        public void UpdateGenre(Genre genre)
        {
            Update(genre);
        }

        public void DeleteGenre(Genre genre)
        {
            Delete(genre);
        }
    }
}