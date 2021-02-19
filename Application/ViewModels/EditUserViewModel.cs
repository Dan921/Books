using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels
{
    public class EditUserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FIO { get; set; }
    }
}
