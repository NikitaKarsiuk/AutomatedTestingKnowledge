using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTestingApplication.Model
{
   [Table("Results")]
   public partial class Results
   {
      public int Id { get; set; }

      [Required]
      public int AmountOfAnswers { get; set; }
      [Required]
      public int AmountOfQuestions { get; set; }
      [Required]
      public TimeSpan Time { get; set; }
      public int UserId { get; set; }
      public int TestId { get; set; }
   }
}
