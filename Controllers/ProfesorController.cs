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
    public class ProfesorController: Controller
    {
        
        private readonly ProfesorInterface _repo;
        private readonly Service _service;
        private readonly IMapper _mapper;


        public ProfesorController(ProfesorInterface repo, IMapper mapper, Service service)
        {
            _service = service;
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<ProfesorDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetProfesores()
        {

            ICollection<Profesor> profesores = _repo.GetProfesores();
            List<ProfesorDto> profesoresDto= _mapper.Map<List<ProfesorDto>>(profesores);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(profesoresDto);
        }

        [HttpGet("Alumnos/{ProfesorId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<AlumnoMinDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetAlumnos(int ProfesorId)
        {

            ICollection<AlumnoMinDto> alumnos = _service.GetAlumnosByProfesorId(ProfesorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(alumnos);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(ProfesorDto))]
        [ProducesResponseType(400)]
        public IActionResult GetProfesorById(int Id)
        {
            if (!_repo.IdExists(Id))
                return NotFound();

            ProfesorDto Profesor = _mapper.Map<ProfesorDto>(_repo.GetProfesorById(Id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Profesor);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult AddProfesor([FromBody] ProfesorDto Profe)
        {
            if (Profe == null)
                return BadRequest(ModelState);

            Profesor Profesor = _repo.ProfesorExists(Profe.FullName);

            if (Profesor != null)
            {
                ModelState.AddModelError("", "Profesor already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Profesor = _mapper.Map<Profesor>(Profe);

            if (!_repo.AddProfesor(Profesor))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        [HttpPost("AddAlumno")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult AddAlumno([FromBody] AlumnoProfeDto ap)
        {
            if (!_service.AddAlumnoProfe(ap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully added");
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProfesor(int Id, [FromBody] ProfesorMinDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (!_repo.IdExists(Id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            Profesor Profesor = _mapper.Map<Profesor>(updated);
            Profesor.Id = Id;

            if (!_repo.UpdateProfesor(Profesor))
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
        public IActionResult DeleteProfesor(int Id)
        {
            if (!_repo.IdExists(Id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_repo.DeleteProfesor(Id))
            {
                ModelState.AddModelError("", "Something went wrong deleting Exam");
            }

            return Ok("Successfully deleted");
        }
    }
}
