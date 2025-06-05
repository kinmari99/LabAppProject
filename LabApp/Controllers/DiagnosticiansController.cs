using LabApp.Dtos;
using LabApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LabApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosticiansController : ControllerBase
    {
        private readonly IDiagnosticianService _diagnosticianService;

        public DiagnosticiansController(IDiagnosticianService diagnosticianService)
        {
            _diagnosticianService = diagnosticianService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DiagnosticianDto>>> GetAll()
        {
            var list = await _diagnosticianService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiagnosticianDto>> GetById(int id)
        {
            var item = await _diagnosticianService.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<DiagnosticianDto>> Create([FromBody] CreateDiagnosticianDto dto)
        {
            var created = await _diagnosticianService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDiagnosticianDto dto)
        {
            var success = await _diagnosticianService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _diagnosticianService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
