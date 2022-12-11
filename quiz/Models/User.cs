using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace quiz.Models
{
    public class User
    {
        [Key]
        public int USERID { get; set; }
        public string Login { get; set; }
        public string NAME { get; set; }

    }
}
