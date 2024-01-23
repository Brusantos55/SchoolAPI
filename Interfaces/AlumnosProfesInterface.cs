using SchoolApi.Entities;
namespace SchoolApi.Interfaces
{
    public interface AlumnosProfesInterface
    {
        public ICollection<AlumnosProfes> GetAllByAlumnoId(int id);
        public ICollection<AlumnosProfes> GetAllByProfesorId(int id);
        bool AddAlumnoProfe(AlumnosProfes ap);
        bool Save();
    }
}