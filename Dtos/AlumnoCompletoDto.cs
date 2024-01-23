namespace SchoolApi.Dtos
{
    public class AlumnoCompletoDto
    {
        public int Id {  get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<ExamenMinDto> Examenes { get; set; }
        public List<ProfesorMinDto> Profesores { get; set; }

    }
}
