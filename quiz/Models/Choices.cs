using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace quiz.Models
{
    public class Choices
    {

        [Key]
        public int ChoiceId { get; set; }
        public bool Is_Correct { get; set; }
        public string Choice { get; set; }
        public int QuestionId { get; set; }

    }
}
