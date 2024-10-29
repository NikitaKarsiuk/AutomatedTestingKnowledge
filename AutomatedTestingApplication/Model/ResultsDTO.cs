using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTestingApplication.Model
{
   internal class ResultsDTO
   {
      public int Id { get; set; }
      public string Surname { get; set; }
      [Required]
      public int AmountOfAnswers { get; set; }
      [Required]
      public int AmountOfQuestions { get; set; }
      [Required]
      public TimeSpan Time { get; set; }
   }
}
