using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace quiz.Models
{
    public class Questions
    {
        [Key]
        public int QuestionID { get; set; }
        public string Question { get; set; }
        public int TestId { get; set; }

    }
}
