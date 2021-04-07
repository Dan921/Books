﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Context
{
    public class BookCover
    {
        public Guid Id { get; set; }

        public byte[] CoverImage { get; set; }
    }
}
