﻿using System;
using System.Collections.Generic;

namespace Repo.App.Entities
{
    public partial class Author
    {
        public Author()
        {
            Book = new HashSet<Book>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
}
