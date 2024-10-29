using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTestingApplication.Model
{
   public class AdminTestDTO
   {
      public int Id { get; set; }
      public string SubjectName { get; set; }
      public string TeacherSurname { get; set; }
   }
}
