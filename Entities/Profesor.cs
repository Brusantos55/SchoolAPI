namespace SchoolApi.Entities
{
    public class Profesor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public string Subject { get; set; }
        public List<AlumnosProfes> Alumnos {  get; set; }
    }
}
