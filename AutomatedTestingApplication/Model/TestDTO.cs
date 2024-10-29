using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTestingApplication.Model
{
   public class TestDTO
   {
      public int Id { get; set; }
      public int TestId { get; set; }
      public string SubjectName { get; set; }

   }
}
