using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTestingApplication.Model
{
   [Table("Log")]
    public class Log
    {
      public int Id { get; set; }
      [Required]
      [StringLength(100)]
      public string LogName { get; set; }
      public DateTime Time { get; set; }
      public int UserId { get; set; }
   }
}
