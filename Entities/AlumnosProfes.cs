namespace SchoolApi.Entities
{
    public class AlumnosProfes
    {
       // public int Id { get; set; }
        public int AlumnoId { get; set; }
        public int ProfesorId { get; set; }
        public virtual Alumno Alumno { get; set; }
        public virtual Profesor Profesor { get; set; }
    }
}
