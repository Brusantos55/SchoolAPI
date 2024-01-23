using Microsoft.Data.SqlClient;
using SchoolApi.Data;
using SchoolApi.Entities;
using SchoolApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SchoolApi.Repositories
{
    public class AlumnoRepository : AlumnoInterface
    {
        private readonly DataContext _context;
        public AlumnoRepository(DataContext context) 
        {
            _context = context;
        }

        public List<Alumno> GetAlumnos()
        {
            return _context.Alumnos.OrderBy(a => a.Id).ToList();
        }

        public Alumno GetAlumnoById(int id)
        {
            return _context.Alumnos.Where(a => a.Id == id).First();
        }

        public Alumno GetBr1()
        {
            string query = $"SELECT * FROM Alumnos WHERE name = @name";
            var p1 = new SqlParameter("@name", "Br1");
            
            Alumno br1Entity = _context.Alumnos.FromSqlRaw(query, p1).First();
            br1Entity.Examenes = _context.Examenes.Where(e => e.AlumnoId == br1Entity.Id).ToList();
            br1Entity.Profesores = _context.AlumnosProfes.Where(ap => ap.AlumnoId == br1Entity.Id).ToList();
            
            return br1Entity; 
        }

        public Alumno AlumnoExists(string name, string surname)
        {
            return GetAlumnos().Where(a => a.Name.Trim().ToUpper().Equals(name.Trim().ToUpper()) 
                                && a.Surname.Trim().Equals(surname.Trim().ToLower())).First();
        }

        public bool IdExists(int Id)
        {
            return _context.Alumnos.Any(p => p.Id == Id);
        }

        public bool AddAlumno(Alumno alumno)
        {
            _context.Add(alumno);
            return Save(); 
        }
        public bool UpdateAlumno(Alumno alumno)
        {
            _context.Update(alumno);
            return Save(); 
        }
        public bool DeleteAlumno(int id)
        {
            Alumno alumno = _context.Alumnos.Where(a => a.Id != id).First();
            _context.Remove(alumno);
            return Save(); 
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
