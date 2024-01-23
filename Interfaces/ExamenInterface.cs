using SchoolApi.Entities;
using SchoolApi.Dtos;

namespace SchoolApi.Interfaces
{
    public interface ExamenInterface
    {
        ICollection<Examen> GetExamenes();
        Examen GetBest();
        Examen GetExamenById(int id);
        ICollection<Examen> GetExamenesByAlumnoId(int id);
        bool AddExamen(Examen examen);
        Examen ExamenExists(ExamenMinDto Examen);
        bool IdExists(int Id);
        bool UpdateExamen(Examen examen);
        bool DeleteExamen(int id);
        bool Save();
    }
}
