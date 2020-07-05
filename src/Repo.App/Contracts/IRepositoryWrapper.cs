using System.Threading.Tasks;

namespace Repo.App.Contracts
{
    public interface IRepositoryWrapper
    {
        IBookRepository Book { get; }
        IAuthorRepository Author { get; }
        IGenreRepository Genre { get; }
        Task Save();
    }
}