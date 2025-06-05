using LabApp.Dtos;
using LabApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LabApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExaminationsController : ControllerBase
    {
        private readonly IExaminationService _examinationService;

        public ExaminationsController(IExaminationService examinationService)
        {
            _examinationService = examinationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ExaminationDto>>> GetAll()
        {
            var exams = await _examinationService.GetAllAsync();
            return Ok(exams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExaminationDto>> GetById(int id)
        {
            var exam = await _examinationService.GetByIdAsync(id);
            if (exam == null)
                return NotFound();

            return Ok(exam);
        }

        [HttpPost]
        public async Task<ActionResult<ExaminationDto>> Create([FromBody] CreateExaminationDto dto)
        {
            var created = await _examinationService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateExaminationDto dto)
        {
            var success = await _examinationService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _examinationService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
