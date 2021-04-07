using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels
{
    public class AuthorsModel
    {
        public IEnumerable<AuthorModel> authors { get; set; }
        public PageModel PageViewModel { get; set; }
    }
}
