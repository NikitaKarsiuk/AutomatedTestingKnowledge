using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomatedTestingApplication.Model
{
   [Table("User")]
   public partial class User
    {
      public int Id { get; set; }
      [Required]
      [StringLength(50)]
      public string Name { get; set; }
      [Required]
      [StringLength(50)]
      public string Surname { get; set; }
      [Required]
      [StringLength(50)]
      public string Patronymic { get; set; }
      [Required]
      [StringLength(50)]
      public string Login { get; set; }
      [Required]
      [StringLength(50)]
      public string Password { get; set; }
      public int RoleId { get; set; }
      public int? GroupId { get; set; }
      public int? SubjectId { get; set; }
   }
}
