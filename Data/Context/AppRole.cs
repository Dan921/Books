using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole(string name)
        {
            this.Name = name;
        }
    }
}
