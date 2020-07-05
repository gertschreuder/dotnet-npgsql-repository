using System;

namespace Repo.App.Entities
{
    public partial class Book
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public long? Isbn { get; set; }
        public Guid AuthorId { get; set; }
        public Guid? GenreId { get; set; }

        public virtual Author Author { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
