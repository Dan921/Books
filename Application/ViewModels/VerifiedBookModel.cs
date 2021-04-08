using Application.Models;
using Application.Models.BookModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels
{
    public class VerifiedBookModel
    {
        public BookModel Book { get; set; }

        public List<BookReviewModel> Reviews { get; set; }
    }
}
