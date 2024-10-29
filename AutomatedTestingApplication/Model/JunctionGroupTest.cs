using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTestingApplication.Model
{
   [Table("JunctionGroupTest")]
   public partial class JunctionGroupTest
    {
      public int Id { get; set; }
      public int TestId { get; set; }
      public int GroupId { get; set; }
      public virtual Test Test { get; set; }
   }
}
