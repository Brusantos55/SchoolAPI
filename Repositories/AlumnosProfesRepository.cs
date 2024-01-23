using SchoolApi.Data;
using SchoolApi.Entities;
using SchoolApi.Interfaces;

namespace SchoolApi.Repositories
{
    public class AlumnosProfesRepository : AlumnosProfesInterface
    {
        private readonly DataContext _context;
        public AlumnosProfesRepository(DataContext context) 
        {
            _context = context;
        }

        public ICollection<AlumnosProfes> GetAllByAlumnoId(int id)
        {
            return _context.AlumnosProfes.Where(ap => ap.AlumnoId == id).ToList();
        }

        public ICollection<AlumnosProfes> GetAllByProfesorId(int id)
        {
            return _context.AlumnosProfes.Where(ap => ap.ProfesorId == id).ToList();
        }

        public bool AddAlumnoProfe(AlumnosProfes ap)
        {
            _context.Add(ap);
            return Save(); 
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}