using AutoMapper;
using SchoolApi.Entities;
using SchoolApi.Dtos;

namespace SchoolApi.Data
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Alumno, AlumnoMinDto>();
            CreateMap<Examen, ExamenMinDto>();
            CreateMap<Profesor, ProfesorMinDto>();
            CreateMap<Alumno, AlumnoDto>();
            CreateMap<Examen, ExamenDto>();
            CreateMap<Profesor, ProfesorDto>();
            CreateMap<AlumnosProfes, AlumnoProfeDto>();


            CreateMap<AlumnoMinDto, Alumno>();
            CreateMap<ExamenMinDto, Examen>();
            CreateMap<ProfesorMinDto, Profesor>();
            CreateMap<AlumnoDto, Alumno>();
            CreateMap<ExamenDto, Examen>();
            CreateMap<ProfesorDto, Examen>();
            CreateMap<AlumnoProfeDto, AlumnosProfes>();

        }
    }
}
