using System.Data.Entity;

namespace AutomatedTestingApplication.Model
{
   public partial class DataContext : DbContext
    {
      public DataContext()
             : base("name=DataContext")
      {
      }

      public virtual DbSet<User> User { get; set; }
      public virtual DbSet<Role> Role { get; set; }
      public virtual DbSet<Group> Group { get; set; }
      public virtual DbSet<Test> Test { get; set; }
      public virtual DbSet<Subject> Subject { get; set; }
      public virtual DbSet<JunctionGroupTest> JunctionGroupTest { get; set; }
      public virtual DbSet<Results> Results { get; set; }
      public virtual DbSet<Question> Question { get; set; }
      public virtual DbSet<Log> Log { get; set; }
   }
}
