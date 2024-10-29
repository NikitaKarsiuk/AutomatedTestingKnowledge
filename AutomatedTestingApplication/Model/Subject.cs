using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTestingApplication.Model
{
   [Table("Subject")]
   public partial class Subject
    {
      public int Id { get; set; }

      [Required]
      [StringLength(50)]
      public string Name { get; set; }
   }
}

