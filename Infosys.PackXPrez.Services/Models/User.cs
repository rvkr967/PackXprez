using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infosys.PackXPrez.Common
{
    public class User
    {
        [Required]
        public string emailId;
        [Required]
        public string password;
    }
}
