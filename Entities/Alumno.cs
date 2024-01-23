namespace SchoolApi.Entities
{
    public class Alumno
    {
        public int Id {  get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Examen> Examenes { get; set; }
        public List<AlumnosProfes> Profesores { get; set; }

    }
}
