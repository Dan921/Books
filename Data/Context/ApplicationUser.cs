using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FIO { get; set; }
    }
}
