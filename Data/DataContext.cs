using Microsoft.EntityFrameworkCore;
using SchoolApi.Entities;
namespace SchoolApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<AlumnosProfes> AlumnosProfes { get; set; }
        public DbSet<Examen> Examenes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Alumno>().HasKey(a => a.Id);
            modelBuilder.Entity<Alumno>().ToTable("Alumnos");

            modelBuilder.Entity<Examen>().HasKey(e => e.Id);
            modelBuilder.Entity<Examen>().ToTable("Examenes");

            modelBuilder.Entity<Profesor>().HasKey(p => p.Id);
            modelBuilder.Entity<Profesor>().ToTable("Profes");

            modelBuilder.Entity<AlumnosProfes>().HasKey(ap => new { ap.AlumnoId, ap.ProfesorId});
            modelBuilder.Entity<AlumnosProfes>().ToTable("AlumnosProfes");

            modelBuilder.Entity<AlumnosProfes>()
                .HasOne(ap => ap.Alumno)
                .WithMany(a => a.Profesores)
                .HasForeignKey(e => e.AlumnoId);
            modelBuilder.Entity<AlumnosProfes>()
                .HasOne(ap => ap.Profesor)
                .WithMany(p => p.Alumnos)
                .HasForeignKey(ap => ap.ProfesorId);

            modelBuilder.Entity<Examen>()
                .HasOne(e => e.Alumno)
                .WithMany(a => a.Examenes)
                .HasForeignKey(e => e.AlumnoId);

        }
    }
}
