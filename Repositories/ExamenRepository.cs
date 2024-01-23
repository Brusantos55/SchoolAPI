using SchoolApi.Interfaces;
using SchoolApi.Entities;
using SchoolApi.Dtos;
using SchoolApi.Data;

namespace SchoolApi.Repositories
{
    public class ExamenRepository : ExamenInterface
    {
        private readonly DataContext _context;
        public ExamenRepository(DataContext context)
        {
            _context = context;
        }

        public Examen GetBest()
        {
            return _context.Examenes.OrderByDescending(e => e.Nota).First();
        }
        public Examen GetExamenById(int id)
        {
            return _context.Examenes.Where(e => e.Id == id).First();
        }

        public ICollection<Examen> GetExamenesByAlumnoId(int id)
        {
            return _context.Examenes.Where(e => e.AlumnoId == id).ToList();
        }

        public ICollection<Examen> GetExamenes()
        {
            return _context.Examenes.OrderBy(e => e.Id).ToList();
        }

        public Examen ExamenExists(ExamenMinDto examen)
        {
            return GetExamenes().Where(a => a.Descripcion.Trim().ToUpper()
                            == examen.Descripcion.Trim().ToUpper()).First();
        }

        public bool IdExists(int Id)
        {
            return _context.Examenes.Any(p => p.Id == Id);
        }

        public bool AddExamen(Examen examen)
        {
            _context.Add(examen);
            return Save();
        }
        public bool UpdateExamen(Examen examen)
        {
            _context.Update(examen);
            return Save();
        }
        public bool DeleteExamen(int id)
        {
            Examen examen = _context.Examenes.Where(_e => _e.Id == id).First();
            
            if (examen != null)
                _context.Remove(examen);
            // else exception
            
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
