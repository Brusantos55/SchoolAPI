using SchoolApi.Entities;

namespace SchoolApi.Interfaces
{
    public interface ProfesorInterface
    {
        ICollection<Profesor> GetProfesores();
        Profesor GetProfesorById(int id);
        bool AddProfesor(Profesor profe);
        Profesor ProfesorExists(string fullname);
        bool IdExists(int Id);
        bool UpdateProfesor(Profesor profe);
        bool DeleteProfesor(int id);
        bool Save();
    }
}
