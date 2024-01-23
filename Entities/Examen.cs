namespace SchoolApi.Entities
{
    public class Examen
    {
        public int Id { get; set; }
        public int Nota { get; set; }
        public int AlumnoId { get; set; }
        public string Descripcion { get; set; }
        public virtual Alumno Alumno { get; set; }

    }
}
