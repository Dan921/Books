using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels
{
    public class ChangePasswordModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
