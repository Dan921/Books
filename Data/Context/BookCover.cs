using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class BookCover
    {
        public Guid Id { get; set; }
        public byte[] CoverImage { get; set; }
    }
}
