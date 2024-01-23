using Microsoft.AspNetCore.Mvc;
using SchoolApi.Interfaces;
using SchoolApi.Entities;
using SchoolApi.Dtos;
using AutoMapper;
using SchoolApi.Services;
namespace SchoolApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlumnoController : Controller
    {
        private readonly AlumnoInterface _AlumnoRepo;
        private readonly Service _service;
        private readonly IMapper _mapper;

        public AlumnoController(AlumnoInterface alumnoInterface, Service service, IMapper mapper)
        {
            _AlumnoRepo = alumnoInterface;
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(AlumnoDto))]
        [ProducesResponseType(400)]
        public IActionResult GetAlumnos()
        {
            List<Alumno> alumnos = _AlumnoRepo.GetAlumnos();
            List<AlumnoDto> alumnosDto= _mapper.Map<List<AlumnoDto>>(alumnos);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(alumnosDto);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(AlumnoDto))]
        [ProducesResponseType(400)]
        public IActionResult GetAlumnoById(int Id)
        {
            if (!_AlumnoRepo.IdExists(Id))
                return NotFound();

            AlumnoDto alumno = _mapper.Map<AlumnoDto>(_AlumnoRepo.GetAlumnoById(Id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(alumno);
        }

        [HttpGet("br1")]
        [ProducesResponseType(200, Type = typeof(AlumnoCompletoDto))]
        [ProducesResponseType(400)]
        public IActionResult GetBr1()
        {
            AlumnoCompletoDto alumno = _service.GetBr1();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(alumno);
        }

        [HttpGet("examenes/{Id}")]
        [ProducesResponseType(200, Type = typeof(List<Examen>))]
        [ProducesResponseType(400)]
        public IActionResult GetExamenes(int Id)
        {
            if (!_AlumnoRepo.IdExists(Id))
                return NotFound();

            ICollection<Examen> examenes = _service.GetExamenesByAlumnoId(Id);
            ICollection<ExamenDto> examenesdto = _mapper.Map<ICollection<ExamenDto>>(examenes);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(examenesdto);
        }

        [HttpGet("profesores/{Id}")]
        [ProducesResponseType(200, Type = typeof(List<Profesor>))]
        [ProducesResponseType(400)]
        public IActionResult GetProfesores(int Id)
        {
            if (!_AlumnoRepo.IdExists(Id))
                return NotFound();

            List<Profesor> profes = _service.GetProfesoresByAlumnoId(Id);
            ICollection<ProfesorDto> profesdto = _mapper.Map<ICollection<ProfesorDto>>(profes);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(profesdto);
        }
            [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult AddAlumno([FromBody] AlumnoMinDto alumnoDto)
        {
            if (alumnoDto == null)
                return BadRequest(ModelState);

            Alumno alumno = _AlumnoRepo.AlumnoExists(alumnoDto.Name,  alumnoDto.Surname);

            if (alumno != null)
            {
                ModelState.AddModelError("", "Alumno already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            alumno = _mapper.Map<Alumno>(alumnoDto);

            if (!_AlumnoRepo.AddAlumno(alumno))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateAlumno(int Id, [FromBody] AlumnoMinDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (!_AlumnoRepo.IdExists(Id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            Alumno alumno = _mapper.Map<Alumno>(updated);
            alumno.Id = Id;

            if (!_AlumnoRepo.UpdateAlumno(alumno))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAlumno(int Id)
        {
            if (!_AlumnoRepo.IdExists(Id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_AlumnoRepo.DeleteAlumno(Id))
            {
                ModelState.AddModelError("", "Something went wrong deleting Alumno");
            }

            return Ok("Successfully deleted");
        }

    }
}
