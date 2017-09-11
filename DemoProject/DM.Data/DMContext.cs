namespace DM.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DMContext : DbContext
    {
        public DMContext()
            : base("name=DMContext")
        {
        }

        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>()
                .HasMany(e => e.Students)
                .WithMany(e => e.Classes)
                .Map(m => m.ToTable("ClassStudent").MapLeftKey("ClassId").MapRightKey("StudentId"));
        }
    }
}
