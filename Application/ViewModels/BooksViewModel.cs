using Application.Models;
using Application.Models.BookModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels
{
    public class BooksViewModel
    {
        public IEnumerable<BookShortModel> books { get; set; }
        public PageModel PageViewModel { get; set; }
    }
}
