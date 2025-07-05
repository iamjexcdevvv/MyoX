using MyoX.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyoX.Domain.Errors
{
    public static class UserAuthError
    {
        public static Error EmailAlreadyExists => new("UserAuthError.EmailAlreadyExists", "Email already exists");
    }
}
