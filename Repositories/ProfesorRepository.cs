using SchoolApi.Interfaces;
using SchoolApi.Entities;
using SchoolApi.Data;

namespace SchoolApi.Repositories
{
    public class ProfesorRepository : ProfesorInterface
    {
        private readonly DataContext _context;
        public ProfesorRepository(DataContext context)
        {
            _context = context;
        }

        public Profesor GetProfesorById(int id)
        {
            return _context.Profesores.Where(e => e.Id == id).First();
        }

        public ICollection<Profesor> GetProfesores()
        {
            return _context.Profesores.OrderBy(e => e.Id).ToList();
        }

        public Profesor ProfesorExists(string fullname)
        {
            return GetProfesores().Where(
                a => a.FullName.Trim().ToUpper().Equals(
                    fullname.Trim().ToUpper())).First();
        }

        public bool IdExists(int Id)
        {
            return _context.Profesores.Any(p => p.Id == Id);
        }

        public bool AddProfesor(Profesor profe)
        {
            _context.Add(profe);
            return Save();
        }
        public bool UpdateProfesor(Profesor profe)
        {
            _context.Update(profe);
            return Save();
        }
        public bool DeleteProfesor(int id)
        {
            Profesor profe = _context.Profesores.Where(_e => _e.Id == id).First();
            
            if (profe != null)
                _context.Remove(profe);
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
