using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace quiz.Models
{
    public class Tests
    {
        [Key]
        public int TestID { get; set; }
        public string Name { get; set; }
        public int CountQuestions { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
