using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTestingApplication.Model
{
   [Table("Question")]
   public partial class Question
    {
      public int Id { get; set; }

      [Required]
      [StringLength(150)]
      public string Name { get; set; }
      [Required]
      [StringLength(400)]
      public string Text { get; set; }
      [Required]
      [StringLength(100)]
      public string Answer { get; set; }
      public int TestId { get; set; }
   }
}
