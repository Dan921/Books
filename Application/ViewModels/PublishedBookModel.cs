using Application.Models;
using Application.Models.BookModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels
{
    public class PublishedBookModel
    {
        public BookShortModel Book { get; set; }

        public List<BookStatusChangeModel> StatusChanges { get; set; }

        public List<BookReviewModel> Reviews { get; set; }

        public int ReadersCount { get; set; }
    }
}
