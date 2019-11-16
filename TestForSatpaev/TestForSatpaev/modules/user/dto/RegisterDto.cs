using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestForSatpaev.modules.user.dto
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
