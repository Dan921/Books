using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class User : IdentityUser<Guid>
    {
        public string FIO { get; set; }
    }
}
