﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class BookGenre
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
