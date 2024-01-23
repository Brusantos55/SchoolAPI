using AutoMapper;
using SchoolApi.Dtos;
using SchoolApi.Entities;
using SchoolApi.Interfaces;

namespace SchoolApi.Services
{
    public class Service
    {
        private readonly IMapper _mapper;
        private readonly AlumnoInterface _alumnoRepo;
        private readonly ExamenInterface _examenRepo;
        private readonly ProfesorInterface _profeRepo;
        private readonly AlumnosProfesInterface _APRepo;

        public Service(
            IMapper mapper,
            ExamenInterface examenRepo, 
            AlumnoInterface alumnoRepo,
            ProfesorInterface profeRepo,
            AlumnosProfesInterface APRepo)
        {
            _mapper = mapper;
            _examenRepo = examenRepo;
            _alumnoRepo = alumnoRepo;
            _profeRepo = profeRepo;
            _APRepo = APRepo;
        }
        
        public Alumno GetCerebrito()
        {
            Examen examen = _examenRepo.GetBest();
            return _alumnoRepo.GetAlumnoById(examen.Id);
        }

        public AlumnoCompletoDto GetBr1(){
            Alumno br1Entity = _alumnoRepo.GetBr1(); 

            List<Profesor> br1Profesores = new List<Profesor>();

            foreach ( AlumnosProfes ap in br1Entity.Profesores)
            {
                Profesor profe = _profeRepo.GetProfesorById(ap.ProfesorId);
                br1Profesores.Add(profe);
            }

            List<ExamenMinDto> br1Examenes = _mapper.Map<List<ExamenMinDto>>(br1Entity.Examenes);
            List<ProfesorMinDto> br1Profes = _mapper.Map<List<ProfesorMinDto>>(br1Profesores);

            AlumnoCompletoDto br1 = new AlumnoCompletoDto
            {
                Id = br1Entity.Id,
                Age = br1Entity.Age,
                Name = br1Entity.Name,
                Surname = br1Entity.Surname,
                Examenes = br1Examenes,
                Profesores = br1Profes
            };

            return br1;
        }
        
        public ICollection<Examen> GetExamenesByAlumnoId(int id){
            return _examenRepo.GetExamenesByAlumnoId(id);
        }

        public bool AddAlumnoProfe(AlumnoProfeDto ap){
            
            if(!(_alumnoRepo.IdExists(ap.AlumnoId) || _profeRepo.IdExists(ap.ProfesorId)))
                return false;

            AlumnosProfes apEntity = _mapper.Map<AlumnosProfes>(ap);

            return _APRepo.AddAlumnoProfe(apEntity);
        }
        
        public List<Profesor> GetProfesoresByAlumnoId(int id){

            ICollection<AlumnosProfes> profesoresIds = _APRepo.GetAllByAlumnoId(id);
            
            List<Profesor> profesores = new List<Profesor>();
            foreach (var ap in profesoresIds)
            {
                Profesor profe = _profeRepo.GetProfesorById(ap.ProfesorId);
                profesores.Add(profe);
            }

            return profesores;
        }

        public List<AlumnoMinDto> GetAlumnosByProfesorId(int id){

            ICollection<AlumnosProfes> aps = _APRepo.GetAllByProfesorId(id);
            
            List<AlumnoMinDto> alumnos = new List<AlumnoMinDto>();
            foreach (var ap in aps)
            {
                Alumno alumno = _alumnoRepo.GetAlumnoById(ap.AlumnoId);
                AlumnoMinDto alumnodto = _mapper.Map<AlumnoMinDto>(alumno);
                alumnos.Add(alumnodto);
            }

            return alumnos;
        }
    }
}
