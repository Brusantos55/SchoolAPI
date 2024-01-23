using Microsoft.AspNetCore.Mvc;
using SchoolApi.Interfaces;
using SchoolApi.Dtos;
using SchoolApi.Entities;
using AutoMapper;

namespace SchoolApi.Controllers
{
    //TODO implementar servicio con errorhandling
    [Route("[controller]")]
    [ApiController]
    public class ExamenController: Controller
    {
        
        private readonly ExamenInterface _repo;
        private readonly IMapper _mapper;

        public ExamenController(ExamenInterface repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Examen>))]
        [ProducesResponseType(400)]
        public IActionResult GetExamenes()
        {

            ICollection<Examen> examenes = _repo.GetExamenes();
            List<ExamenDto> examenesDto= _mapper.Map<List<ExamenDto>>(examenes);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(examenesDto);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(ExamenMinDto))]
        [ProducesResponseType(400)]
        public IActionResult GetExamenById(int Id)
        {
            if (!_repo.IdExists(Id))
                return NotFound();

            ExamenMinDto examen = _mapper.Map<ExamenMinDto>(_repo.GetExamenById(Id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(examen);
        }

        [HttpGet("best")]
        [ProducesResponseType(200, Type = typeof(ExamenMinDto))]
        [ProducesResponseType(400)]
        public IActionResult GetBest()
        {
            ExamenMinDto best = _mapper.Map<ExamenMinDto>(_repo.GetBest());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(best);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult AddExamen([FromBody] ExamenMinDto examenDto)
        {
            if (examenDto == null)
                return BadRequest(ModelState);

            Examen examen = _repo.ExamenExists(examenDto);

            if (examen != null)
            {
                ModelState.AddModelError("", "examen already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            examen = _mapper.Map<Examen>(examenDto);

            if (!_repo.AddExamen(examen))
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
        public IActionResult UpdateExamen(int Id, [FromBody] ExamenMinDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (!_repo.IdExists(Id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            Examen examen = _mapper.Map<Examen>(updated);
            examen.Id = Id;

            if (!_repo.UpdateExamen(examen))
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
        public IActionResult DeleteExamen(int Id)
        {
            if (!_repo.IdExists(Id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_repo.DeleteExamen(Id))
            {
                ModelState.AddModelError("", "Something went wrong deleting Exam");
            }

            return Ok("Successfully deleted");
        }
    }
}
