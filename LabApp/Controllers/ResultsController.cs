using LabApp.Dtos;
using LabApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LabApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly IResultService _resultService;

        public ResultsController(IResultService resultService)
        {
            _resultService = resultService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ResultDto>>> GetAll()
        {
            var results = await _resultService.GetAllAsync();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResultDto>> GetById(int id)
        {
            var result = await _resultService.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<List<ResultDto>>> GetByOrder(int orderId)
        {
            var results = await _resultService.GetByOrderIdAsync(orderId);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<ResultDto>> Create([FromBody] CreateResultDto dto)
        {
            var result = await _resultService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateResultDto dto)
        {
            var success = await _resultService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _resultService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
