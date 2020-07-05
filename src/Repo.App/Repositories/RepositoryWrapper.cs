using System.Threading.Tasks;
using Repo.App.Contracts;

namespace Repo.App.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly SampleContext _repositoryContext;
        private IBookRepository _book;
        private IAuthorRepository _author;
        private IGenreRepository _genre;

        public IBookRepository Book => _book ??= new BookRepository(_repositoryContext);
        public IAuthorRepository Author => _author ??= new AuthorRepository(_repositoryContext);

        public IGenreRepository Genre => _genre ??= new GenreRepository(_repositoryContext);

        public RepositoryWrapper(SampleContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task Save()
        {
            await _repositoryContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}