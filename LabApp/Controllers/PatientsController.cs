using LabApp.Dtos;
using LabApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabApp.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class PatientsController : ControllerBase
        {
            private readonly IPatientService _patientService;

            public PatientsController(IPatientService patientService)
            {
                _patientService = patientService;
            }
        [Authorize]
        [HttpGet]
            public async Task<ActionResult<List<PatientDto>>> GetAll()
            {
                var patients = await _patientService.GetAllAsync();
                return Ok(patients);
            }
        [Authorize]
        [HttpGet("{id}")]
            public async Task<ActionResult<PatientDto>> GetById(int id)
            {
                var patient = await _patientService.GetByIdAsync(id);
                if (patient == null)
                    return NotFound();

                return Ok(patient);
            }
        [Authorize(Roles = "Admin")]
        [HttpPost]
            public async Task<ActionResult<PatientDto>> Create([FromBody] CreatePatientDto dto)
            {
                var created = await _patientService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, [FromBody] UpdatePatientDto dto)
            {
                var success = await _patientService.UpdateAsync(id, dto);
                if (!success)
                    return NotFound();

                return NoContent();
            }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var success = await _patientService.DeleteAsync(id);
                if (!success)
                    return NotFound();

                return NoContent();
            }
        }
    }

