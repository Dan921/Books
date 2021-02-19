using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels
{
    public class AuthorsViewModel
    {
        public IEnumerable<AuthorModel> authors { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
