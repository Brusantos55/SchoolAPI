using SchoolApi.Entities;
namespace SchoolApi.Interfaces
{
    public interface AlumnoInterface
    {
        bool IdExists(int Id);
        Alumno AlumnoExists(string name, string surname);
        List<Alumno> GetAlumnos();
        Alumno GetBr1();
        Alumno GetAlumnoById(int id);
        bool AddAlumno(Alumno alumno);
        bool UpdateAlumno(Alumno alumno);
        bool DeleteAlumno(int id);
        bool Save();
    }
}
