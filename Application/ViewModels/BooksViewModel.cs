using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels
{
    public class BooksViewModel
    {
        public IEnumerable<BookShortModel> books { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
