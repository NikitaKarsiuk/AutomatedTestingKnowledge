using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTestingApplication.Model
{
   [Table("Test")]
   public partial class Test
    {
      public int Id { get; set; }
      [Required]
      [StringLength(150)]
      public string Name { get; set; }
      public int UserId { get; set; }
      public virtual User User { get; set; }
   }
}

